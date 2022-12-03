using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private GameObject player;

    [SerializeField] 
    private GameObject checkpoints;
    private Transform[] respawnPoints;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPoints = checkpoints.GetComponentsInChildren<Transform>().Skip(1).ToArray();
        player.transform.position = respawnPoints[PlayerPrefs.GetInt("currentCheckpoint")].position;
    }
}
