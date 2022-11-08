using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private PlayerInputHandler playerInput;
    private Player player;

    private float playerGravity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInputHandler>();
        player = GetComponent<Player>();

        playerGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && playerInput.JumpInputStop)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;      
        }

        
        // Set the gravity scale to half at the peak of the player's jump.
        if(rb.velocity.y == 0 && player.StateMachine.CurrentState == player.InAirState && !playerInput.JumpInputStop)
        {
            rb.gravityScale = playerGravity * 0.5f;
        }
        // Set the gravity scale back to normal once not at the peak or the jump button is let go.
        else if(rb.velocity.y != 0 || player.StateMachine.CurrentState != player.InAirState || playerInput.JumpInputStop)
        {
            rb.gravityScale = playerGravity;
        }

    }
}
