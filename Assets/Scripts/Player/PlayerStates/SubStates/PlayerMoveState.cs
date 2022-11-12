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
			float targetSpeed = xInput * playerData.movementVelocity;
			// Accelerate if there is player input. Else, decelerate
			float accelRate = (xInput == 0) ? playerData.deceleration : playerData.acceleration;

			float speedDif = targetSpeed - player.RB.velocity.x;
			float movement = speedDif * accelRate;

			player.RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
		}
		else
		{
            // Without Acceleration
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
