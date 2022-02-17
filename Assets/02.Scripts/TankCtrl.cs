using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCtrl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;

    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -5.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        tr.Translate(Vector3.forward * Time.deltaTime * speed * v);
        tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);
    }
}
