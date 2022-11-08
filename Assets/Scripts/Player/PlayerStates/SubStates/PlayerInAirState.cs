using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {

	protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

	private Movement movement;
	private CollisionSenses collisionSenses;

	//---Input
	private int xInput;
	private bool jumpInput;
	private bool jumpInputStop;
	private bool grabInput;
	private bool dashInput;
	private bool dodgeInput;

	//---Checks
	private bool isGrounded;
	private bool isTouchingWall;
	private bool isTouchingWallBack;
	private bool oldIsTouchingWall;
	private bool oldIsTouchingWallBack;
	private bool isTouchingLedge;

	private bool coyoteTime;
	private bool wallJumpCoyoteTime;
	private bool isJumping;

	private float startWallJumpCoyoteTime;

	public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {
	}

	public override void DoChecks() 
	{
		base.DoChecks();

		oldIsTouchingWall = isTouchingWall;
		oldIsTouchingWallBack = isTouchingWallBack;

		if (CollisionSenses) {
			isGrounded = CollisionSenses.Ground;
			isTouchingWall = CollisionSenses.WallFront;
			isTouchingWallBack = CollisionSenses.WallBack;
			isTouchingLedge = CollisionSenses.LedgeHorizontal;
		}
	}

	public override void Enter() 
	{
		base.Enter();
	}

	public override void Exit() 
	{
		base.Exit();

		oldIsTouchingWall = false;
		oldIsTouchingWallBack = false;
		isTouchingWall = false;
		isTouchingWallBack = false;
	}

	public override void LogicUpdate() 
	{
		base.LogicUpdate();

		CheckCoyoteTime();

		xInput = player.InputHandler.NormInputX;
		jumpInput = player.InputHandler.JumpInput;
		jumpInputStop = player.InputHandler.JumpInputStop;

		CheckJumpMultiplier();

		if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
		{
			stateMachine.ChangeState(player.LandState);
		}
		else if (jumpInput && player.JumpState.CanJump())
		{
			stateMachine.ChangeState(player.JumpState);
		}
		else if (jumpInput && !player.JumpState.CanJump())
		{
			stateMachine.ChangeState(player.GlideState);
		}
		else
		{
			Movement?.CheckIfShouldFlip(xInput);
			Movement?.SetVelocityX(playerData.movementVelocity * xInput);

			player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
			player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
		}
	}

	private void CheckJumpMultiplier() 
	{
		if (isJumping) {
			if (jumpInputStop) 
			{
				Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
				isJumping = false;
			} 
			else if (Movement.CurrentVelocity.y <= 0f) 
			{
				isJumping = false;
			}
		}
	}

	public override void PhysicsUpdate() 
	{
		base.PhysicsUpdate();
	}

	private void CheckCoyoteTime() 
	{
		if (coyoteTime && Time.time > startTime + playerData.coyoteTime) 
		{
			coyoteTime = false;
			player.JumpState.DecreaseAmountOfJumpsLeft();
		}
	}

	public void StartCoyoteTime() => coyoteTime = true;

	public void SetIsJumping() => isJumping = true;
}
