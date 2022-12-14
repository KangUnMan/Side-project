using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathRunWinner : MonoBehaviour
{
    public static bool DeathRunGameRoundEnd;
    public GameObject ResultPnl;
    public TMP_Text WinnerNick;
    public PhotonView PV;
    string winnernickname;
    private int count = 0;

    string message = string.Empty;
    Queue queue;
    private void Awake()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Players.Count);
        queue = new Queue(PhotonNetwork.CurrentRoom.Players.Count);
        Debug.Log(queue.size());
    }
    private void Update()
    {
        if (DeathRunGameRoundEnd)
        {
            DeathRunGameRoundEnd = false;
            DeathRunEndGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            count++;
            queue.push(other.gameObject.GetComponent<PlayerScript>().nickname);
            Debug.Log("Queue push");
            if(count >= PhotonNetwork.CurrentRoom.Players.Count || queue.isFull())
            {
                DeathRunGameRoundEnd = true;
            }
            other.gameObject.GetComponent<PlayerScript>().Death = true;

        }
    }
    void DeathRunEndGame()
    {
        PV.RPC("DeathRunEndGameRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void DeathRunEndGameRPC()
    {
        string win = winnernickname;
        int rank = 0;
        while (!queue.isEmpty())
        {
            rank++;
            message += rank+"등은 "+queue.pop()+" 님\n";
        }
        WinnerNick.SetText(message);
        ResultPnl.SetActive(true);

    }


    

}
