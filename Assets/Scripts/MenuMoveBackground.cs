using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class MenuMoveBackground : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        Debug.Log($"{startPos.x - transform.position.x}, {GetComponent<RectTransform>().sizeDelta.x}");

        if (startPos.x - transform.position.x > GetComponent<RectTransform>().sizeDelta.x)
        {
            transform.position = startPos;
        }
    }
}
