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
            winnernickname =other.gameObject.GetComponent<PlayerScript>().nickname;
            DeathRunGameRoundEnd = true;
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
        WinnerNick.SetText(win+"님 우승 축하드립니다!");
        ResultPnl.SetActive(true);

    }

}
