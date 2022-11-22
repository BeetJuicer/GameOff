using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AreaEffector2D))]
public class TestPush : MonoBehaviour
{
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.color = origColor;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, GetComponent<AreaEffector2D>().forceAngle) * Vector2.right);

        DrawArrow.ForGizmo(transform.position, dir * 10f, 1f);
    }
}
