using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Transform> points = new List<Transform>();
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);

        int idx = Random.Range(1, points.Count);

        Vector3 pos = points[idx].position;

        PhotonNetwork.Instantiate("Tank",
                                  pos,
                                  Quaternion.identity,
                                  0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
