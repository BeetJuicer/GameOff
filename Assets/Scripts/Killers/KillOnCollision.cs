using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Juice - impact, play death animation, sound, pEffects
            //Make a splash square
            SceneManager.LoadScene("Main");
        }
    }
}
