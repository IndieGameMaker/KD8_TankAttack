using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text roomInfo;

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

    void DisplayRoomInfo()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;

        string msg = $"{currentRoom.Name} (<color=#ff0000>{currentRoom.PlayerCount}</color>/<color=#00ff00>{currentRoom.MaxPlayers}</color>)";

        roomInfo.text =
    }
}
