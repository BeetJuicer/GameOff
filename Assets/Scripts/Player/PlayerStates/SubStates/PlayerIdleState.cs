using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState {
	public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	private bool dashInput;
	private bool dodgeInput;

	public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
		Movement?.SetVelocityX(0f);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		if (!isExitingState)
		{

			if (xInput != 0)
			{
				stateMachine.ChangeState(player.MoveState);
			}
		}
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
