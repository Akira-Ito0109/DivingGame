using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerScript playerController;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerController.transform.position;
    }

    private void Update()
    {
        if (playerController.inWater==true)
        {
            return;
        }

        if(playerController != null)
        {
            transform.position = playerController.transform.position + offset;
        }
    }
}
