using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 04f2788e-b8a4-4cb2-b720-8a578d3509de

    // 게임버전
    private readonly string gameVersion = "1.0";
    // 유저명
    public string userId = "LeeJaeHyun";

    // UserID, Room Name InputField
    public TMP_InputField userId_IF;
    public TMP_InputField roomName_IF;

    void Awake()
    {
        // 접속 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = userId;

        // 방장이 씬을 로딩했을 때 자동으로 씬이 호출되는 기능
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            // 포톤서버에 접속
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    void Start()
    {
        // 유저명 로딩
        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0, 1000):0000}");
        userId_IF.text = userId;

        // 유저명 설정
        PhotonNetwork.NickName = userId;
    }

    #region PUN_CALLBACK

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
        // PhotonNetwork.JoinRandomRoom();
    }

    // 랜덤조인 실패했을 때 호출되는 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode}, message={message}");

        // 룸 속성을 정의
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        // 룸 생성
        PhotonNetwork.CreateRoom("MyRoom", ro);
    }

    // 룸을 생성한 후 호출
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성됨 !");
    }

    // 룸에 입장한 후 호출
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료 !!!");

        // BattleField 씬으로 전환(씬 로딩)
        if (PhotonNetwork.IsMasterClient)
        {
            //SceneManagements.SceneManager.LoadScene
            PhotonNetwork.LoadLevel("BattleField");
        }
    }

    public GameObject roomPrefab;
    public Transform contentTr;  // 차일드화 시킬 부모의 Transform
    // 룸 목록을 저장할 Dictionary 생성
    public Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    // 룸 목록이 변경됐을 때마다 호출되는 콜백
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;

        foreach (var room in roomList)
        {
            Debug.Log(room.Name + " " + room.PlayerCount);

            if (room.RemovedFromList == true) // 룸 삭제 여부
            {
                if (roomDict.TryGetValue(room.Name, out tempRoom))
                {
                    // 삭제 로직
                    Destroy(tempRoom);
                    // 딕셔너리 데이터 삭제
                    roomDict.Remove(room.Name);
                }
            }
            else // 룸이 처음 생성됐거나 갱신될 룸
            {
                // 처음 생성하는 룸
                if (roomDict.ContainsKey(room.Name) == false)
                {
                    // Room 프리팹 생성
                    GameObject _room = Instantiate(roomPrefab, contentTr);
                    // 룸 정보 설정
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    // 딕셔너리에 룸 추가
                    roomDict.Add(room.Name, _room);
                }
                else
                {
                    // 룸 정보 갱신 로직
                    if (roomDict.TryGetValue(room.Name, out tempRoom))
                    {
                        tempRoom.GetComponent<RoomData>().RoomInfo = room;
                    }
                }
            }

        }
    }

    #endregion

    #region UI_CALLBACK
    public void OnLoginButtonClick()
    {
        SetUserId();
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnMakeRoomButtonClick()
    {
        SetUserId();

        // 룸 명이 없을 경우
        if (string.IsNullOrEmpty(roomName_IF.text))
        {
            roomName_IF.text = $"ROOM_{Random.Range(0, 999):000}";
        }

        // 룸 속성을 정의
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        // 룸 생성
        PhotonNetwork.CreateRoom(roomName_IF.text, ro);
    }
    #endregion

    #region USER_DIFINE_FUNC

    void SetUserId()
    {
        if (string.IsNullOrEmpty(userId_IF.text))
        {
            userId = $"USER_{Random.Range(0, 1000):0000}";
            userId_IF.text = userId;
        }
        userId = userId_IF.text;

        // PlayerPrefs 사용해서 UserId를 저장
        PlayerPrefs.SetString("USER_ID", userId);
        PhotonNetwork.NickName = userId;
    }

    #endregion
}
