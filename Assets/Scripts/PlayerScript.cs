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

    private Rigidbody rb;

    private float x;
    private float z;


    private Vector3 straightRotation = new Vector3(180, 0, 0);

    private int score;



    [SerializeField, Header("水しぶきのエフェクト")]
    private GameObject waterEffectPrefab = null;

    [SerializeField, Header("水しぶきのSE")]
    private AudioClip splashSE = null;

    [SerializeField]
    private Text txtScore;


    void Start()
    {
        rb = GetComponent<Rigidbody>();


        transform.eulerAngles = straightRotation;  


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


}