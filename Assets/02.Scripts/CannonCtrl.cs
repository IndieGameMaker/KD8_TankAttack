using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        float angle = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 50.0f;
        tr.Rotate(Vector3.right * angle); //tr.Rotate(angle,0,0)
    }
}
