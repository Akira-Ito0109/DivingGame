using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Penguin;

    [SerializeField]
    private GameObject Goal;

    private float distance;

    private void Update()
    {
        //ペンギンと水面のy座標の差を距離とする
        distance = Penguin.transform.position.y - Goal.transform.position.y;


        if(distance<0)
        {
            distance = 0;
        }

        Debug.Log(distance);
    }
}
