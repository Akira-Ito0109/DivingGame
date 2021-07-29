using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerObj.transform.position;
    }

    private void Update()
    {
        if(playerObj !=null)
        {
            transform.position = playerObj.transform.position + offset;
        }
    }
}
