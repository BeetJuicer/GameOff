using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject endMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        endMenu.SetActive(true);
    }
}
