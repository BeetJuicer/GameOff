using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Cinemachine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject endMenu;

    [SerializeField]
    private GameObject roomCam;
    [SerializeField]
    private GameObject endCamera;

    [SerializeField]
    private GameObject player;
    private Animator playerAnimator;

    [SerializeField]
    private AnimationClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerAnimator = player.GetComponent<Animator>();
        Timing.RunCoroutine(_EndScene());
    }

    private IEnumerator<float> _EndScene()
    {
        GameManager.instance.isInputDisabled = true;
        yield return Timing.WaitForSeconds(0.1f);

        roomCam.SetActive(false);
        endCamera.SetActive(true);

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerAnimator.Play(clip.name, -1, 0f);
        AudioManager.instance.Stop("BG_Theme");
        AudioManager.instance.Play("Victory");

        yield return Timing.WaitForSeconds(2f);
        endMenu.SetActive(true);
        yield return Timing.WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }
}
