using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static List<GameObject> players = new List<GameObject>();

    public int[] score;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            
        }
      
    }

    public void PlayerScores() //배열 생성
    {
        if (SceneManager.GetActiveScene().name == "GameSelect" && PhotonNetwork.IsMasterClient)
        {
         //   players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log("플레이어 배열 생성");
         //   score = new int[players.Length];
            Debug.Log("스코어  배열 생성");

        }
    }
    public void EndGame()
    {
        if (PhotonNetwork.IsMasterClient)
           {
             

           // for (int i = 0; i < players.Length; i++)
          //  {
          //      score[i] += players[i].GetComponent<PlayerScript>().score;
          //  }
        }
    }
}
