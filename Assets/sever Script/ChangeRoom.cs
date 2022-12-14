using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnLobbyChangeBtn()
    {
        if (PhotonNetwork.IsMasterClient) //방장만
        {
            PhotonNetwork.LoadLevel("GameSelect");  //씬 이동
        }
    }

    public void OnChangeBtn()
    {
        if (PhotonNetwork.IsMasterClient) //방장만
        {
            PhotonNetwork.LoadLevel("TFGunStage");  //씬 이동
        }
    }

    public void OnChangeDeathBtn()
    {
        if (PhotonNetwork.IsMasterClient) //방장만
        {

            PhotonNetwork.LoadLevel("DeathRun");  //씬 이동

        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
