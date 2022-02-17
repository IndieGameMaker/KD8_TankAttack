using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 게임버전
    private readonly string gameVersion = "1.0";
    // 유저명
    public string userId = "LeeJaeHyun";

    void Awake()
    {
        // 접속 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = userId;

        // 방장이 씬을 로딩했을 때 자동으로 씬이 호출되는 기능
        PhotonNetwork.AutomaticallySyncScene = true;

        // 포톤서버에 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    // 포톤 서버(클라우드)에 접속했을 때 호출되는 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버접속완료");

        // 로비 접속
        PhotonNetwork.JoinLobby();
    }

    // 로비에 접속 완료됐을 때 호출하는 콜백
    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 입장");

        // 랜덤한 방에 입장 요청
        PhotonNetwork.JoinRandomRoom();
    }

    // 랜덤조인 실패했을 때 호출되는 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode}, message={message}");
    }
}
