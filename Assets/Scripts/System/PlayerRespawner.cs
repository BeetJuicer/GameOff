using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private GameObject player;

    [SerializeField] 
    private GameObject checkpoints;
    private Transform[] respawnPoints;

    private void Awake()
    {
        //checkpoint starts at 1
        PlayerPrefs.SetInt("currentCheckpoint", 1);
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPoints = checkpoints.GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = respawnPoints[PlayerPrefs.GetInt("currentCheckpoint")].position;
    }
}
