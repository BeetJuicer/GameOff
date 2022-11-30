using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPad : MonoBehaviour
{
    [SerializeField]
    private float springPushForce;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //play animation
            //set current velocity to 0 before adding push
            collision.GetComponent<Rigidbody2D>().velocity *= Vector2.right;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * springPushForce, ForceMode2D.Impulse);
        }
    }
}
