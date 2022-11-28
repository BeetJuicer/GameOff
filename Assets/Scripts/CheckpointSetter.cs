using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    [SerializeField] private int checkpointCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetCheckpoint();
            Debug.Log(PlayerPrefs.GetInt("checkpointCount"));
        }
    }
    
    private void SetCheckpoint()
    {
        if (PlayerPrefs.GetInt("currentCheckpoint") < checkpointCount)
        {
            PlayerPrefs.SetInt("currentCheckpoint", checkpointCount);
        }
    }
}
