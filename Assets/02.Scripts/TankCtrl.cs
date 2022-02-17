#pragma warning disable IDE0044, IDE0051, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class TankCtrl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    private PhotonView pv;
    private CinemachineVirtualCamera vCam;
    private AudioSource audio;

    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");

    public float speed = 20.0f;
    public GameObject cannon;
    public Transform firePos;
    public AudioClip fireSfx;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        pv = GetComponent<PhotonView>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        if (pv.IsMine)
        {
            vCam.Follow = vCam.LookAt = tr;
            rb.centerOfMass = new Vector3(0, -5.0f, 0);
        }
        else
        {
            rb.isKinematic = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine == true)
        {
            Move();

            if (Input.GetMouseButtonDown(0))
            {
                // Fire(); // 일반함수로 호출
                // RPC 호출
                pv.RPC("Fire", RpcTarget.AllViaServer);
            }
        }
    }

    void Move()
    {
        tr.Translate(Vector3.forward * Time.deltaTime * speed * v);
        tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);
    }

    /*
        RPC (Remote Procedure Call) , RMI (Remote Method Invoke)
    
    */

    [PunRPC]
    void Fire()
    {
        audio.PlayOneShot(fireSfx, 0.8f);

        var obj = Instantiate(cannon, firePos.position, firePos.rotation);
        Destroy(obj, 5.0f);
    }
}
