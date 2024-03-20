using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMPro.TMP_InputField NickNameInput;
    public GameObject LoadingtPnl;
    public GameObject RespawnPanel;
    private readonly string gameVersion = "v1.0"; // readonly 속성은 bool 속성임

     void Awake()  // Awake()는 게임이 시작되기전에, 모든 변수와 게임상태를 초기화하기위해서 호출됨 (start보다 빠르다.)
    {   // 방장이 혼자 씬을 로딩하면 , 나머지 사람들은 자동으로 싱크가 됨
        PhotonNetwork.AutomaticallySyncScene = true;

        // 게임 버전 지정
        PhotonNetwork.GameVersion = gameVersion;

        Screen.SetResolution(940, 540, false);
        // 화면 크기 설정
        PhotonNetwork.SendRate = 60; // 넣으면 서버 동기화가 더 빨리 됨

        PhotonNetwork.SerializationRate = 30;

        //서버 접속 
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnstartBtn()
    {
       
            PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
            PhotonNetwork.LocalPlayer.SetScore(0); // 스코어 초기화
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); //JoinOrCreateRoom 은 룸이 존재 하지 않는다면 룸을 생성
       

        //PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // 닉네임 인풋필드에 적은걸로 부여
        
    }
    public override void OnJoinedRoom() // 방에 입장했을때 호출됨
    {
        LoadingtPnl.SetActive(false); // 패널 안보이게

        if(PhotonNetwork.IsMasterClient) //방장만
        {
            PhotonNetwork.LocalPlayer.NickName += "(host)";
            PhotonNetwork.LoadLevel("GameSelect");  //씬 이동
        }
    }

    public override void OnConnectedToMaster()
    {
        LoadingtPnl.SetActive(false);
        RespawnPanel.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) // 만약 esc를 눌럿을때 서버에 연결되었다면
            PhotonNetwork.Disconnect(); //서버연결  끊는 메소드 호출 
    }

    public override void OnDisconnected(DisconnectCause cause) //Photon 서버 와의 연결을 끊은 후 호출
    {
        LoadingtPnl.SetActive(true); 
        RespawnPanel.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
}
