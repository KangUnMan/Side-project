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
    public GameObject LoadingtPnl;
    public GameObject RespawnPanel;
    private readonly string gameVersion = "v1.0"; // readonly �Ӽ��� bool �Ӽ���

     void Awake()  // Awake()�� ������ ���۵Ǳ�����, ��� ������ ���ӻ��¸� �ʱ�ȭ�ϱ����ؼ� ȣ��� (start���� ������.)
    {   // ������ ȥ�� ���� �ε��ϸ� , ������ ������� �ڵ����� ��ũ�� ��
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� ���� ����
        PhotonNetwork.GameVersion = gameVersion;

        Screen.SetResolution(960, 540, false);
        // ȭ�� ũ�� ����
        PhotonNetwork.SendRate = 60; // ������ ���� ����ȭ�� �� ���� ��

        PhotonNetwork.SerializationRate = 30;

        //���� ���� 
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnstartBtn()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // �г��� ��ǲ�ʵ忡 �����ɷ� �ο� 
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); //JoinOrCreateRoom �� ���� ���� ���� �ʴ´ٸ� ���� ����
    }
    public override void OnJoinedRoom() // �濡 ���������� ȣ���
    {
        LoadingtPnl.SetActive(false); // �г� �Ⱥ��̰�

        if(PhotonNetwork.IsMasterClient) //���常
        {
            PhotonNetwork.LocalPlayer.NickName += "(��)";
            PhotonNetwork.LoadLevel("Stage 1");  //�� �̵�
        }
    }

    public override void OnConnectedToMaster()
    {
        LoadingtPnl.SetActive(false);
        RespawnPanel.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) // ���� esc�� �������� ������ ����Ǿ��ٸ�
            PhotonNetwork.Disconnect(); //��������  ���� �޼ҵ� ȣ�� 
    }

    public override void OnDisconnected(DisconnectCause cause) //Photon ���� ���� ������ ���� �� ȣ��
    {
        LoadingtPnl.SetActive(true); 
        RespawnPanel.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
}
