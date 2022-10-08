using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMPro.TMP_InputField NickNameInput;
    public GameObject DisconnectPnl;
    public GameObject RespawnPanel;

     void Awake()  // Awake()는 게임이 시작되기전에, 모든 변수와 게임상태를 초기화하기위해서 호출됨 (start보다 빠르다.)
    {
        Screen.SetResolution(960, 540, false);    // 화면 크기 설정
        PhotonNetwork.SendRate = 60; // 넣으면 서버 동기화가 더 빨리 됨
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // 커넥트 함수를 호출하면 서버에 연결이됨

    public override void OnConnectedToMaster() //메소드는 PUN 이 준비 되었을 때 호출 됨 (서버 연결되면 호출되는듯)   이 메소드가 호출 될 때 저수준 연결이 된 것으로 PUN 이 AppId , user 등을 백그라운드에서 전송 할 것 입니다. 
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // 닉네임 인풋필드에 적은걸로 부여 
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); //JoinOrCreateRoom 은 룸이 존재 하지 않는다면 룸을 생성
    }

    public override void OnJoinedRoom() // 방에 입장했을때 호출됨
    {   
        DisconnectPnl.SetActive(false); // 패널 안보이게
        Spawn();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) // 만약 esc를 눌럿을때 서버에 연결되었다면
            PhotonNetwork.Disconnect(); //서버연결  끊는 메소드 호출 
    }

    public override void OnDisconnected(DisconnectCause cause) //Photon 서버 와의 연결을 끊은 후 호출
    {
        DisconnectPnl.SetActive(true); 
        RespawnPanel.SetActive(false);
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        RespawnPanel.SetActive(false);
    }
}
