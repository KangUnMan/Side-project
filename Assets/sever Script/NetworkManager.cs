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

     void Awake()  // Awake()�� ������ ���۵Ǳ�����, ��� ������ ���ӻ��¸� �ʱ�ȭ�ϱ����ؼ� ȣ��� (start���� ������.)
    {
        Screen.SetResolution(960, 540, false);    // ȭ�� ũ�� ����
        PhotonNetwork.SendRate = 60; // ������ ���� ����ȭ�� �� ���� ��
        PhotonNetwork.SerializationRate = 30;
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings(); // Ŀ��Ʈ �Լ��� ȣ���ϸ� ������ �����̵�

    public override void OnConnectedToMaster() //�޼ҵ�� PUN �� �غ� �Ǿ��� �� ȣ�� �� (���� ����Ǹ� ȣ��Ǵµ�)   �� �޼ҵ尡 ȣ�� �� �� ������ ������ �� ������ PUN �� AppId , user ���� ��׶��忡�� ���� �� �� �Դϴ�. 
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text; // �г��� ��ǲ�ʵ忡 �����ɷ� �ο� 
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null); //JoinOrCreateRoom �� ���� ���� ���� �ʴ´ٸ� ���� ����
    }

    public override void OnJoinedRoom() // �濡 ���������� ȣ���
    {   
        DisconnectPnl.SetActive(false); // �г� �Ⱥ��̰�
        Spawn();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) // ���� esc�� �������� ������ ����Ǿ��ٸ�
            PhotonNetwork.Disconnect(); //��������  ���� �޼ҵ� ȣ�� 
    }

    public override void OnDisconnected(DisconnectCause cause) //Photon ���� ���� ������ ���� �� ȣ��
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
