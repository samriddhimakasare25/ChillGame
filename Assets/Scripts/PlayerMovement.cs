using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;

    public Rigidbody2D playerRB;
    public float speed = 400f;  // Fixed the missing semicolon

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        // Corrected the input action for movement
        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Move.canceled += ctx =>
        {
            direction = 0;  // Stop moving when the input is released
        };
    }

    void Update()
    {
        // Update the velocity based on the direction input
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
    }
}
