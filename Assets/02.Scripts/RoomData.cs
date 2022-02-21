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

    void Start()
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
        }
    }


}
