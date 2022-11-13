using Baracuda.Monitoring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

    public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		Movement?.CheckIfShouldFlip(xInput);

		if (playerData.movementUsesAcceleration)
		{
			player.HandleAcceleration();
		}
		else
		{
            Movement?.SetVelocityX(playerData.movementVelocity * xInput);
        }

		// Only switch to idle state if done decelerating.
		if (xInput == 0 && Mathf.Abs(player.RB.velocity.x) <= 0.5f)
		{
			stateMachine.ChangeState(player.IdleState);
		}
    }

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
