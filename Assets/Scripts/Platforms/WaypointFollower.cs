using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaypointFollower : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 platformGizmosOrigin;

    [SerializeField] private Vector2[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(startPos + waypoints[currentWaypointIndex], transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        //transform.position = Vector2.MoveTowards(transform.position, startPos + waypoints[currentWaypointIndex], Time.deltaTime * speed);
        transform.DOMove(startPos + waypoints[currentWaypointIndex], 1f);
    }

    private void OnDrawGizmos()
    {
        platformGizmosOrigin = (Application.isPlaying) ? startPos : (Vector2)transform.position;

        foreach (Vector2 waypoint in waypoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(platformGizmosOrigin + waypoint, 0.5f);
        }
    }
}