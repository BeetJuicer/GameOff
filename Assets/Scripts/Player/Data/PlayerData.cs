using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public bool movementUsesAcceleration = true;
    public bool doConserveMomentum = true;
    public float movementVelocity = 10f;
    public float acceleration = 1f;
    public float deceleration = 1f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float inAirAcceleration = 1;
    public float inAirDeceleration = 1;

    [Header("Glide State")]
    public float glideFallVelocity = -1.5f;
    public float glideVelocityMultiplier = 1f;
}
