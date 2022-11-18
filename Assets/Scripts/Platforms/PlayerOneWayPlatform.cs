using Baracuda.Monitoring;
using System.Collections;
using UnityEngine;

public class PlayerOneWayPlatform : MonitoredBehaviour
{
    //[Monitor]
    public GameObject currentOneWayPlatform { get; private set; }
    public bool isPassingThroughPlatform { get; private set; }

    [SerializeField]
    private float collisionDisableTime = 0.25f;

    private BoxCollider2D playerCollider;
    private PlayerMovement playerMovement;

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Awake()
    {
        base.Awake();

        playerCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.NormInputY < 0)
        {
            if (currentOneWayPlatform != null)
            {
                isPassingThroughPlatform = true;
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);

        yield return new WaitForSeconds(collisionDisableTime);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        isPassingThroughPlatform = false;
    }
}