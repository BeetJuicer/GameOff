using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerGlideState : PlayerAbilityState
{
    private float origGravity;

    public PlayerGlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Awake()
    {
        base.Awake();
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

        Movement?.SetVelocityY(1f);

        // Set ability done to true after glide duration or glide input is stopped or is grounded
        isAbilityDone = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = origGravity;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
