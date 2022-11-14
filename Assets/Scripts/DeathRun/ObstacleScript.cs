using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObstacleScript : MonoBehaviour
{

    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if (!PV.IsMine && col.gameObject.tag == "Player" && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            col.gameObject.GetComponent<PlayerScript>().Hit(); //플레이어 사라짐

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
