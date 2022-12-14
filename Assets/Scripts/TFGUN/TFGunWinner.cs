using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;

public class TFGunWinner : MonoBehaviour
{
    public static int PlaysCount;// 들어온 인원 수
    public static bool TFGameRoundEnd;
    public GameObject ResultPnl;
    public PhotonView PV;
    int count = 0;
    private void Awake()
    {
           
       
    }
    // Update is called once per frame

    private void Start()
    {
        PlaysCount = PhotonNetwork.CountOfPlayers; //초기에 설정
        Debug.Log(PlaysCount);
    }
    void Update()
    {
       if(PlaysCount==0&&PhotonNetwork.IsMasterClient&&count==0)
        {
            TFGameRoundEndGame();
            count++;
        }
        
    }

    static public void EndMyTFGame()
    {
        PlaysCount--;
        Debug.Log(PlaysCount);
    }

    void TFGameRoundEndGame()
    {
        PV.RPC("TFGameRoundEndGameRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TFGameRoundEndGameRPC()
    {
        ResultPnl.SetActive(true);

    }
}
