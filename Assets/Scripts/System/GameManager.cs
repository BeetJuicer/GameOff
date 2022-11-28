using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGamePaused { get; private set; }

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
        pauseMenu.SetActive(false);
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
}
