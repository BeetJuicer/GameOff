using System.Collections;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    public GameObject currentOneWayPlatform { get; private set; }

    private BoxCollider2D playerCollider;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.NormInputY < 0 && playerMovement.LastPressedJumpTime > 0)
        {
            Debug.Log(0);
            if (currentOneWayPlatform != null)
            {   
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
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}