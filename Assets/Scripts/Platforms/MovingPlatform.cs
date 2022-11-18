using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //TODO: Import player momentum and play around with it.

    // PLATFORM
    private Rigidbody2D rb;
    private PlatformState _state = PlatformState.Idle;

    // PLAYER
    //private PlayerMomentum _player;
    private PlayerMovement _player;
    private Transform _playerParent;
    private bool _isPassenger;

    [Header("Player")]
    [SerializeField] private float momentumForce;
    [SerializeField] private float momentumBufferTime;
    private float _momentumTime;

    [Header("Movement")]
    [SerializeField] private Vector2 relativeTargetPos;
    private Vector2 startPos;
    private Vector2 endPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float reachedTargetTheshold;
    [Space(10)]
    [SerializeField] private float minIdleTime;
    private float _timeInIdle;
    private Vector2 _moveDir;

    [Header("Layers & Tags")]
    [SerializeField] private string playerTag;

    private void Start()
    {

        _player = FindObjectOfType<PlayerMovement>();
        //_player = FindObjectOfType<PlayerMomentum>();
        _playerParent = _player.transform.parent;
        rb = GetComponent<Rigidbody2D>();

        startPos = transform.position;
        endPos = (Vector2)transform.position + relativeTargetPos;
        _state = PlatformState.Idle;
    }

    private void FixedUpdate()
    {
        // State Machine
        switch (_state)
        {
            case PlatformState.Idle:
                _timeInIdle += Time.deltaTime;
                _momentumTime += Time.deltaTime;

                _moveDir = Vector2.zero;

                // Guard Clause: prevents changing state before we've been in idle for "minIdleTime"
                if (_timeInIdle < minIdleTime)
                    break;

                // Checks whether we're on the start or end position, to decide whether we must return or check if we should go forward
                if (CheckDistance(transform.position, endPos, reachedTargetTheshold))
                {
                    _state = PlatformState.Returning;
                    _timeInIdle = 0;
                }
                else if (_isPassenger && _player.LastOnGroundTime > 0)
                {
                    // Changes to moving if the player is standing on this
                    _state = PlatformState.Moving;
                    _timeInIdle = 0;
                }
                break;

            case PlatformState.Moving:
                //Move towards endPos at constant speed
                transform.position = Vector2.MoveTowards(transform.position, endPos, moveSpeed);
                //_moveDir = (endPos - (Vector2)transform.position).normalized;

                // Stop when we get to endPos
                if (CheckDistance(transform.position, endPos, reachedTargetTheshold))
                {
                    _state = PlatformState.Idle;
                   // _player.AddMomentum(momentumForce * (endPos - startPos).normalized, momentumBufferTime);
                    _momentumTime = 0;
                }
                break;

            case PlatformState.Returning:
                //Move back to startPos at constant speed
                transform.position = Vector2.MoveTowards(transform.position, startPos, returnSpeed);
                //_moveDir = (startPos - (Vector2)transform.position).normalized;

                // Stop when we get to startPos
                if (CheckDistance(transform.position, startPos, reachedTargetTheshold))
                    _state = PlatformState.Idle;
                break;
        }
    }

    private bool CheckDistance(Vector2 pos, Vector2 goal, float theshold)
    {
        // True when positions are close enough
        return (Vector2.Distance(pos, goal) < theshold);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            _player.transform.parent = transform;
            _isPassenger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            _player.transform.parent = _playerParent;
            _isPassenger = false;
        }
    }

    private enum PlatformState
    {
        Idle,
        Moving,
        Returning
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(startPos, 0.25f);
            Gizmos.DrawSphere(endPos, 0.25f);
            Gizmos.DrawLine(startPos, endPos);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.DrawSphere(transform.position + (Vector3)relativeTargetPos, 0.25f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)relativeTargetPos);
        }
    }
}
