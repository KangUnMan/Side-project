using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PhotonView pv;

    public Transform[] spawnPoints;

    private Hashtable CP;

    private void Awake()
    {
        

        if (instance = null){
            instance = this;
            DontDestroyOnLoad(this.gameObject); //파괴하지않을 게임 오브젝트
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
      
        pv.GetComponent<PhotonView>();
        CP = PhotonNetwork.LocalPlayer.CustomProperties;
        CreatePlayer();
        if(SceneManager.GetActiveScene().name=="TFGunStage")
        {   
            for(int i=0; i<4; i++)
            {
                CreateRock();
            } 
        }
    }

    void CreatePlayer()
    {
        

        GameObject playerTemp = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-30f, 30f),0,0), Quaternion.identity);

        int colorNum = (int)CP["Color"];
        if(colorNum != 1)
        {
            playerTemp.GetComponent<PlayerScript>().InitColor(colorNum - 1);
        }
    }

    void CreateRock()
    {
        GameObject rocktemp = PhotonNetwork.Instantiate("ItemRock", new Vector3(Random.Range(-40f, 40f), 0, Random.Range(-40f, 40f)), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
