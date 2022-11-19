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

    private void Start()
    {
        if (!gameObject.GetComponent<Collider2D>().isTrigger)
            gameObject.GetComponent<Collider2D>().isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        origColor = spriteRenderer.color;

        source = gameObject.transform.Find("Source");
        target = gameObject.transform.Find("Target");
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
        collision.GetComponent<Rigidbody2D>().AddForce(pushForce * GetPushDirection(), ForceMode2D.Force);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = origColor;
        }
    }

    private Vector2 GetPushDirection()
    {
        Vector2 direction;

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

        return direction;
    }
}
