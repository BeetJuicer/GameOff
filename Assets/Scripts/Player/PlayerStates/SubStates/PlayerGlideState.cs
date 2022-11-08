using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Movement?.SetVelocityY(1f);
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
