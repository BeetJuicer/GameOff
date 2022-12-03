using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void StartGame()
    {
        Timing.RunCoroutine(_LoadScene());
    }

    private IEnumerator<float> _LoadScene()
    {
        yield return Timing.WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
