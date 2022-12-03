using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGamePaused { get; private set; }
    public bool isGameOver { get; private set; }

    [HideInInspector]
    public bool isInputDisabled;

    [SerializeField] 
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        isGamePaused = false;
        isGameOver = false;
        isInputDisabled = false;
        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        AudioManager.instance.Play("BG_Theme");
    }

    public void OnEscapeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isGamePaused = !isGamePaused;
            pauseMenu.SetActive(isGamePaused);

            if (isGamePaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    public void PlayerDeath()
    {
        isGameOver = true;
        StartCoroutine("DeathRoutine");
    }

    private IEnumerator DeathRoutine()
    {
        AudioManager.instance.Stop("BG_Theme");
        // Wait a little to let the death animation play.
        yield return new WaitForSeconds(0.1f);

        //Freeze the game
        Time.timeScale = 0;

        //Let the death audio play before resetting the scene
        AudioManager.instance.Play("Death");
        yield return new WaitForSecondsRealtime(0.3f);

        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}
