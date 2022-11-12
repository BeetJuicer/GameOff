using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGlideState : PlayerAbilityState
{
    private float origGravity;

    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;

    public PlayerGlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        origGravity = player.RB.gravityScale; 
        player.RB.gravityScale = 0;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Set X velocity to x input * movementSpeed / glideMovement Speed
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        Movement?.CheckIfShouldFlip(xInput);

        // Adjust
        Movement?.SetVelocityX(playerData.movementVelocity * playerData.glideVelocityMultiplier * xInput);
        Movement?.SetVelocityY(playerData.glideFallVelocity);

        // Set ability done to true after glide duration or glide input is stopped or is grounded
        if (isGrounded || jumpInputStop)
        {
            isAbilityDone = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.RB.gravityScale = origGravity;
    }
}
