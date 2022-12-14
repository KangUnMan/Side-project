using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using TMPro;

public class GoldCupGet : MonoBehaviour
{
    string winnernickname;
    public GameObject game;
    public TMP_Text WinnerNick;
    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            winnernickname =other.gameObject.GetComponent<PlayerScript>().nickname;
            TFGAMEEndGame();
        }
    }

    void TFGAMEEndGame()
    {
        PV.RPC("TFGAMEEndGameRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TFGAMEEndGameRPC()
    {
        string win = winnernickname;
        WinnerNick.SetText(winnernickname + "님의 우승을 축하합니다!!");
        game.SetActive(true);

    }
}
