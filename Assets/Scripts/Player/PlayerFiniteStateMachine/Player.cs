using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Baracuda.Monitoring;
using UnityEngine.InputSystem.XInput;

public class Player : MonitoredBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerGlideState GlideState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    private CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    #endregion

    #region Other Variables         

    private Vector2 workspace;
    [Monitor]
    private string state;
    #endregion

    #region Unity Callback Functions
    
    // OnDestroy and Awake for Monitoring
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Awake()
    {
        base.Awake();
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        GlideState = new PlayerGlideState(this, StateMachine, playerData, "glide");

    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        // Monitor the current state.
        state = StateMachine.CurrentState.ToString();

        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    public void HandleAcceleration()
    {
        int xInput = InputHandler.NormInputX;
        bool isGrounded = CollisionSenses.Ground;

        float targetSpeed = xInput * playerData.movementVelocity;
        float targetDirection = Mathf.Sign(targetSpeed);

        #region Acceleration Rate
        float accelRate;
        // Accelerate if there is player input. Else, decelerate
        if (isGrounded)
        {
            accelRate = (xInput == 0) ? playerData.deceleration : playerData.acceleration;
        }
        else
        {
            accelRate = (xInput == 0) ? playerData.inAirDeceleration : playerData.inAirAcceleration;
        }
        #endregion

        #region Conserve Momentum
        // We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
        if (playerData.doConserveMomentum && xInput == targetDirection && xInput != 0 && !isGrounded)
        {
            // Prevent any deceleration from happening.
            if (Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed))
            {
                accelRate = 0;
            }
        }
        #endregion

        #region Jump Apex Acceleration
        if (StateMachine.CurrentState == InAirState && Mathf.Abs(RB.velocity.y) < 0.2f)
        {
            accelRate *= playerData.jumpApexAccelerationMultiplier;
            targetSpeed *= playerData.jumpApexSpeedMultiplier;
        }
        #endregion

        float speedDif = targetSpeed - RB.velocity.x;
        float movement = speedDif * accelRate;

        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }   

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
