using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState {
	protected int xInput;
	protected int yInput;

	protected bool isTouchingCeiling;

	protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private Movement movement;

	protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
	private CollisionSenses collisionSenses;

	private bool JumpInput;
	protected bool isGrounded;

	public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void DoChecks() {
		base.DoChecks();

		if (CollisionSenses) {
			isGrounded = CollisionSenses.Ground;
		}
	}

	public override void Enter() {
		base.Enter();

		player.JumpState.ResetAmountOfJumpsLeft();
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

		xInput = player.InputHandler.NormInputX;
		yInput = player.InputHandler.NormInputY;
		JumpInput = player.InputHandler.JumpInput;

		if (JumpInput && player.JumpState.CanJump()) {
			stateMachine.ChangeState(player.JumpState);
		} else if (!isGrounded) {
			player.InAirState.StartCoyoteTime();
			stateMachine.ChangeState(player.InAirState);
		}

	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}
