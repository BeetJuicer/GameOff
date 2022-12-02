using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class SpringPad : MonoBehaviour
{
    [SerializeField]
    private float springPushForce;

    [SerializeField]
    private AnimationClip pushAnim;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play(pushAnim.name, -1, 0f);
            //set current y velocity to 0 before adding push
            collision.GetComponent<Rigidbody2D>().velocity *= Vector2.right;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * springPushForce, ForceMode2D.Impulse);
        }
    }
}
