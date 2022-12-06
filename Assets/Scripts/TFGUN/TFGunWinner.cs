using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TFGunWinner : MonoBehaviour
{
    public int PlaysCount;// 들어온 인원 수
    private void Awake()
    {
        PlaysCount = PhotonNetwork.CountOfPlayers; //초기에 설정
}
    // Update is called once per frame
    void Update()
    {

    }

   
}
