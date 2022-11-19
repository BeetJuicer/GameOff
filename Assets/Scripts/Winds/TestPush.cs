using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TestPush : MonoBehaviour
{

    private Transform source;
    private Transform target;

    private SpriteRenderer spriteRenderer;
    private Color origColor;

    [SerializeField]
    private float pushForce = 20f;

    Vector2 direction;

    private void Start()
    {
        if (!gameObject.GetComponent<Collider2D>().isTrigger)
            gameObject.GetComponent<Collider2D>().isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        origColor = spriteRenderer.color;

        source = gameObject.transform.Find("Source");
        target = gameObject.transform.Find("Target");

        GetPushDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = Color.green;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().IsGliding)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(pushForce * direction, ForceMode2D.Force);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = origColor;
        }
    }

    private void GetPushDirection()
    {
        if (source.position.y == target.position.y)
        {
            direction.y = 0;
        }
        else
        {
            direction.y = 1 * Mathf.Sign(target.position.y - source.position.y);
        }

        if (source.position.x == target.position.x)
        {
            direction.x = 0;
        }
        else
        {
            direction.x = 1 * Mathf.Sign(target.position.x - source.position.x);
        }
    }
}
