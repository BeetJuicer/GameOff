using Baracuda.Monitoring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonitoredBehaviour
{
    [Monitor]
    private Vector2 velocity;

    private Rigidbody2D rb;
    private PlayerInputHandler playerInput;
    private Player player;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float playerGravity;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInputHandler>();
        player = GetComponent<Player>();

        playerGravity = rb.gravityScale;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = rb.velocity;

        // Increase gravity when falling.
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && playerInput.JumpInputStop)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;      
        }
        
        // Set the gravity scale to half at the peak of the player's jump.
        if(Mathf.Abs(rb.velocity.y) < .3f && player.StateMachine.CurrentState == player.InAirState && !playerInput.JumpInputStop)
        {
            Debug.Log("Halved gravity");
            rb.gravityScale = playerGravity * 0.5f;
        }
        // Set the gravity scale back to normal once not at the peak or the jump button is let go.
        else if(rb.velocity.y != 0 || player.StateMachine.CurrentState != player.InAirState || playerInput.JumpInputStop)
        {
            Debug.Log("Normal gravity");
            rb.gravityScale = playerGravity;
        }
    }
}
