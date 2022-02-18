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

        DisplayRoomInfo();
    }

    void DisplayRoomInfo()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;

        string msg = $"{currentRoom.Name} (<color=#ff0000>{currentRoom.PlayerCount}</color>/<color=#00ff00>{currentRoom.MaxPlayers}</color>)";
        roomInfo.text = msg;

        // string msg1 = $"[0] (<color=#ff0000>[1]</color>/<color=#00ff00>[2]</color>)";
        // roomInfo.text = string.Format(msg,
        //                               currentRoom.Name,
        //                               currentRoom.PlayerCount,
        //                               currentRoom.MaxPlayers);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DisplayRoomInfo();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        DisplayRoomInfo();
    }
}
