using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    [Header("落下速度")]
    public float fallSpeed;

    private Rigidbody rb;

    private float x;
    private float z;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Debug.Log(x);
        Debug.Log(z);

        rb.velocity = new Vector3(x * moveSpeed, -fallSpeed, z * moveSpeed);

        Debug.Log(rb.velocity);
    }
}
