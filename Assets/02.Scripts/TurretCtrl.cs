using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurretCtrl : MonoBehaviour
{
    private Transform tr;
    private RaycastHit hit;
    private PhotonView pv;

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = tr.root.GetComponent<PhotonView>();

        this.enabled = pv.IsMine;
    }

    void Update()
    {
        // 마우스 커서의 위치를 기반으로 3D Ray를 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            //터렛기준으로 월드좌표를 로컬좌표로 변환
            Vector3 pos = tr.InverseTransformPoint(hit.point);
            // Atan2 두 벡터간의 사잇각을 계산
            float angle = Mathf.Atan2(pos.x, pos.z) * Mathf.Rad2Deg;

            tr.Rotate(Vector3.up * Time.deltaTime * 10.0f * angle);
        }
    }
}
