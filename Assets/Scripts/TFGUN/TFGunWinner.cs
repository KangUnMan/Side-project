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
    public GameObject GoldCup;
    public PhotonView PV;
    int count = 0;
    Queue queue;
    private void Awake()
    {
        queue = new Queue(PhotonNetwork.CurrentRoom.Players.Count);
    }
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(PlayerCountStart());
    }
    void Update()
    {

       if(PlaysCount==1&&count==0)
        {
            GoldCup.SetActive(true);
            //TFGameRoundEndGame();
            count++;
        }
        
    }

    static public void EndMyTFGame()
    {
        PlaysCount--;
        Debug.Log(PlaysCount);
    }

    IEnumerator PlayerCountStart()
    {
        yield return new WaitForSeconds(3.0f);
        PlaysCount = PhotonNetwork.CurrentRoom.Players.Count;


        Debug.Log(PlaysCount);
    }
}
