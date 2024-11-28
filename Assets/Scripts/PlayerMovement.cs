using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;

    public Rigidbody2D playerRB;
    public float speed = 400f;
    bool isFacingRight = true;
    public float jumpForce = 5;
    bool isGrounded;
    int numberOfJumps = 0; // Corrected variable name
    public Transform groundCheck;
    public LayerMask groundLayer;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Move.canceled += ctx =>
        {
            direction = 0;  // Stop moving when the input is released
        };

        controls.Land.Jump.performed += ctx => Jump();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        // Update the velocity based on the direction input
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        // Flip the character based on the direction of movement
        if (isFacingRight && direction < 0)
            Flip();
        else if (!isFacingRight && direction > 0)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0; // Reset jump count when grounded
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); // Fixed method name
            numberOfJumps++;
        }
        else
        {
            if (numberOfJumps == 1) // Allow a second jump if the player is in the air
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); // Fixed method name
                numberOfJumps++;
            }
        }
    }
}
