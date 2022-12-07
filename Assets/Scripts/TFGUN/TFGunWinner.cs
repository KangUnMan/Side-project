using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class TFGunWinner : MonoBehaviour
{
    public static int PlaysCount;// 들어온 인원 수
    private void Awake()
    {
        PlaysCount = PhotonNetwork.CountOfPlayers; //초기에 설정
    }
    // Update is called once per frame
    void Update()
    {

    }

    static public void EndMyTFGame()
    {
        if (PlaysCount == 4)
        {
            PhotonNetwork.LocalPlayer.AddScore(4);
        }
        else if(PlaysCount == 3)
        {
            PhotonNetwork.LocalPlayer.AddScore(6);
        }
        else if (PlaysCount == 2)
        {
            PhotonNetwork.LocalPlayer.AddScore(8);
        }
        else if (PlaysCount == 1)
        {
            PhotonNetwork.LocalPlayer.AddScore(10);
        }
        PlaysCount--;
    }
   
}
