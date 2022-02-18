#pragma warning disable IDE0044, IDE0051, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Cinemachine;
using TMPro;
using UnityEngine.UI;

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
    public TMP_Text userId;
    public Image hpBar;

    private float initHp = 100.0f;  //초기 생명수치
    private float currHp = 100.0f;  //현재 HP

    // 탱크의 모든 MeshRenderer 컴퍼넌트를 저장
    private MeshRenderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        pv = GetComponent<PhotonView>();
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<MeshRenderer>();

        if (pv.IsMine)
        {
            vCam.Follow = vCam.LookAt = tr;
            rb.centerOfMass = new Vector3(0, -5.0f, 0);
        }
        else
        {
            rb.isKinematic = true;
        }

        // 자신의 NickName 표기
        userId.text = pv.Owner.NickName;
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
                pv.RPC("Fire", RpcTarget.AllViaServer, pv.Owner.ActorNumber);
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
    void Fire(int shooterId)
    {
        audio.PlayOneShot(fireSfx, 0.8f);

        var obj = Instantiate(cannon, firePos.position, firePos.rotation);
        obj.GetComponent<Cannon>().shooterId = shooterId;
        Destroy(obj, 5.0f);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("CANNON"))
        {
            currHp -= 20.0f;
            hpBar.fillAmount = currHp / initHp;

            // ActorNumber(shooterId) -> NickName
            int shooterId = coll.gameObject.GetComponent<Cannon>().shooterId;
            Player shooter = PhotonNetwork.CurrentRoom.GetPlayer(shooterId);


            if (currHp <= 0.0f)
            {
                if (pv.IsMine)
                {
                    string msg = $"<color=#00ff00>[{pv.Owner.NickName}]</color> 님이 <color=#ff0000>[{shooter.NickName}]</color>에게 살해 당했습니다.";
                }

                TankDestroy();
            }
        }
    }

    void TankDestroy()
    {
        // 탱크를 Invisible
        SetVisible(false);
        Invoke("RespwanTank", 3.0f);
    }

    void RespwanTank()
    {
        currHp = initHp;
        hpBar.fillAmount = 1.0f;

        // 랜덤한 좌표이동 로직

        SetVisible(true);
    }

    void SetVisible(bool isVisible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = isVisible;
        }

        tr.Find("Canvas").gameObject.SetActive(isVisible);
        //tr.Find("Canvas").GetComponent<Canvas>().enabled = isVisible;
    }
}
