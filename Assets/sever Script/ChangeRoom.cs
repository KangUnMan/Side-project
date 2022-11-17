using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ChangeRoom : MonoBehaviour
{
    public static bool GameFinsh;
    bool MapChanged=false;
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnChangeBtn()
    {
        if (PhotonNetwork.IsMasterClient) //방장만
        {
            
            PhotonNetwork.LoadLevel("TFGunStage");  //씬 이동
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFinsh&&!MapChanged)
        {
            PhotonNetwork.LoadLevel("WinnerStage");
            MapChanged = true;
        }
    }
}
