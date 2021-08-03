using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Penguin;

    [SerializeField]
    private GameObject Goal;

    [SerializeField]
    private Text txtDistance;

    private float distance;

    private void Update()
    {
        //ペンギンと水面のy座標の差を距離とする
        distance = Penguin.transform.position.y - Goal.transform.position.y;


        if(distance<0)
        {
            distance = 0;
        }

        //Debug.Log(distance);

        txtDistance.text = distance.ToString("F2");
    }
}
