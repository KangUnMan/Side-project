using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RockScript : MonoBehaviourPunCallbacks
{

    public PhotonView PV;
    public float power = 1500f;
    int hit;
    // Start is called before the first frame update

    private void Awake()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
    void Start()
    {
        Destroy(gameObject, 3.5f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(!PV.IsMine && col.gameObject.tag == "Player" && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            col.gameObject.GetComponent<PlayerScript>().Hit(); //플레이어 사라짐
                
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
