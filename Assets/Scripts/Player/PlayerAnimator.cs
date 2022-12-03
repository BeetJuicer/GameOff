using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement mov;
    private Animator anim;

    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }

    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        anim = mov.gameObject.GetComponent<Animator>();

        anim.SetBool("death", false);
    }

    private void Update()
    {
        SetAnimationBools();
    }

    private void LateUpdate()
    {
        CheckAnimationState();
    }

    private void CheckAnimationState()
    {       
        if (startedJumping)
        {
            AudioManager.instance.Play("Jump");
            startedJumping = false;
            return;
        }

    }

    private void SetAnimationBools()
    {
        anim.SetBool("death", GameManager.instance.isGameOver);
        anim.SetBool("wallSlide", mov.IsSliding);
        anim.SetBool("fall", mov.RB.velocity.y < 0 && mov.LastOnGroundTime < 0 && !mov.IsSliding);
        anim.SetBool("jump", mov.IsJumping);
        anim.SetBool("move", mov.IsRunning && !mov.IsJumping);
        anim.SetBool("idle", !mov.IsRunning && !mov.IsJumping && !mov.IsSliding && mov.RB.velocity.y == 0);
    }
}
