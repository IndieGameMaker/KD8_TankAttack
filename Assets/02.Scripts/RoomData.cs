using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomData : MonoBehaviour
{
    public TMP_Text roomText;
    private RoomInfo roomInfo;

    void Awake()
    {
        roomText = GetComponentInChildren<TMP_Text>();
    }

    // 프로퍼티 정의
    public RoomInfo RoomInfo
    {
        //Getter ex) Debug.Log(RoomData.RoomInfo);
        get
        {
            return roomInfo;
        }
        //Setter ex) RoomData.RoomInfo = {데이터};
        set
        {
            roomInfo = value;
            // 룸 정보 표시
            roomText.text = $"{roomInfo.Name} ({roomInfo.PlayerCount}/{roomInfo.MaxPlayers})";

            // 버튼 클릭시 호출할 이벤트를 연결
            // 1. 델리게이트
            // 2. 람다식
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(
                delegate ()
                {
                    OnEnterRoom(roomInfo.Name);
                }
            );
        }
    }

    void OnEnterRoom(string roomName)
    {
        // 룸 속성 정의
        // RoomOptions ro = new RoomOptions();
        // ro.IsOpen = true;
        // ro.IsVisible = true;
        // ro.MaxPlayers = 20;

        // PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);

        PhotonNetwork.JoinRoom(roomName);
    }
}
