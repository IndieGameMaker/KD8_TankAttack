using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject expEffect;
    public float speed = 2000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
    }

    void OnCollisionEnter()
    {
        GameObject obj = Instantiate(expEffect,
                                     transform.position,
                                     Quaternion.identity);

        Destroy(obj, 5.0f);

        Destroy(this.gameObject);
    }

}
