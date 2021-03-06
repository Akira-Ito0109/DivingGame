using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    [Header("落下速度")]
    public float fallSpeed;

    [Header("着水判定用。trueなら着水済")]
    public bool inWater;

    public enum AttitudeType
    {
        Straight,Prone
    }

    [Header("現在のキャラの姿勢")]
    public AttitudeType attitudeType;

    private Rigidbody rb;

    private float x;
    private float z;


    private Vector3 straightRotation = new Vector3(180, 0, 0);

    private int score;

    private Vector3 proneRotation = new Vector3(-90, 0, 0);

    [SerializeField, Header("水しぶきのエフェクト")]
    private GameObject waterEffectPrefab = null;

    [SerializeField, Header("水しぶきのSE")]
    private AudioClip splashSE = null;

    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Button btnChangeAttitude;


    void Start()
    {
        rb = GetComponent<Rigidbody>();


        transform.eulerAngles = straightRotation;

        attitudeType = AttitudeType.Straight;

        btnChangeAttitude.onClick.AddListener(ChangeAttitude);

    }

    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x * moveSpeed, -fallSpeed, z * moveSpeed);
    }


    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Water" && inWater == false)
        {

            inWater = true;

            GameObject effect = Instantiate(waterEffectPrefab, transform.position, Quaternion.identity);
            effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y, effect.transform.position.z - 0.5f);

            Destroy(effect, 2.0f);

            AudioSource.PlayClipAtPoint(splashSE, transform.position);


            StartCoroutine(OutOfWater());
        }
        if (col.gameObject.tag == "FlowerCircle")
        {

            score += col.transform.parent.GetComponent<FlowerCircle>().point;

            txtScore.text = score.ToString();

        }
    }



    private IEnumerator OutOfWater()
    {
        yield return new WaitForSeconds(1.0f);

        rb.isKinematic = true;

        transform.eulerAngles = new Vector3(-30, 180, 0);

        transform.DOMoveY(4.7f, 1.0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAttitude();
        }
    }

    private void ChangeAttitude()
    {
        switch(attitudeType)
        {
            case AttitudeType.Straight:
                attitudeType = AttitudeType.Prone;

                transform.DORotate(proneRotation, 0.25f, RotateMode.WorldAxisAdd);

                rb.drag = 25.0f;

                btnChangeAttitude.transform.GetChild(0).DORotate(new Vector3(0, 0, 180), 0.25f);

                break;

            case AttitudeType.Prone:

                attitudeType = AttitudeType.Straight;

                transform.DORotate(straightRotation, 0.25f);

                rb.drag = 0f;

                btnChangeAttitude.transform.GetChild(0).DORotate(new Vector3(0, 0, 90), 0.25f);

                break;
        }
    }
}