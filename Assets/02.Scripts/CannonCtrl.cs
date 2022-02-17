using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CannonCtrl : MonoBehaviour
{
    private Transform tr;
    private PhotonView pv;

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = tr.root.GetComponent<PhotonView>();

        this.enabled = pv.IsMine;
        //this.enabled = tr.root.GetComponent<Photon.Pun.PhotonView>().IsMine;
    }

    void Update()
    {
        float angle = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 50.0f;
        tr.Rotate(Vector3.right * angle); //tr.Rotate(angle,0,0)
    }
}
