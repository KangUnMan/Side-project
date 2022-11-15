using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RockitemScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DestroyRPC();
        }
    }

    void DestroyRPC()
    {
        Destroy(gameObject);
    }
}
