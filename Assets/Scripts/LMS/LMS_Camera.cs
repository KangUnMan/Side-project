using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMS_Camera : MonoBehaviour
{
    Vector3 location;
    public GameObject target;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        location = target.transform.position;
        if(Player.transform.rotation.eulerAngles.y == 180)
        {
            location.z -= 1;
        }
        else
        {
            location.z += 1;
        }
        transform.position = location;
        transform.rotation = target.transform.rotation;
        //transform.LookAt(target.transform);

    }
}
