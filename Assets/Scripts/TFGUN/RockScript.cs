using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RockScript : MonoBehaviourPunCallbacks
{

    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
