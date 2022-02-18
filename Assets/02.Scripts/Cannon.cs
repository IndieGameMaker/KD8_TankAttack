using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject expEffect;
    public float speed = 2000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
    }

    void OnCollisionEnter()
    {
        Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

        GameObject obj = Instantiate(expEffect,
                                     transform.position,
                                     rot);

        Destroy(obj, 5.0f);

        Destroy(this.gameObject);
    }

}
