using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;

public class TFGunWinner : MonoBehaviour
{
    public static int PlaysCount=99;// 들어온 인원 수
    public static bool TFGameRoundEnd;
    bool CountOn;
    public GameObject ResultPnl;
    public PhotonView PV;
    int count = 0;
    private void Awake()
    {
           
       
    }
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(PlayerCountStart());
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

    IEnumerator PlayerCountStart()
    {
        yield return new WaitForSeconds(3.0f);
        PlaysCount = PhotonNetwork.CurrentRoom.Players.Count;


        Debug.Log(PlaysCount);
    }
}
