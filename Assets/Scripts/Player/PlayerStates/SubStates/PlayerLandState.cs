using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else //if (isAnimationFinished) TODO: -- return this for when land animations are implemented.
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }       
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAnimationFinished = true;
    }
}
