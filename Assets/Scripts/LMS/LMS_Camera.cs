using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMS_Camera : MonoBehaviour
{
    Vector3 location;
    public GameObject target;
    public GameObject Player;

    private const float rotateCamXAxisSpeed = 5.0f;
    private const float rotateCamYAxisSpeed = 5.0f;

    private const float limitMinX = -30;
    private const float limitMaxX = 30;
    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;
    private float mouseX;
    private float mouseY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse Y");
            mouseY = Input.GetAxis("Mouse X");
            eulerAngleX += rotateCamXAxisSpeed * -mouseX;
            eulerAngleY += rotateCamYAxisSpeed * mouseY;
            if (eulerAngleX > 360)
            {
                eulerAngleX -= 360;
            }
            else if (eulerAngleX < -360)
            {
                eulerAngleX += 360;
            }
            eulerAngleX = Mathf.Clamp(eulerAngleX, limitMinX, limitMaxX);
            Player.GetComponent<Transform>().rotation = Quaternion.Euler(0, eulerAngleY, 0);
            Player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, eulerAngleY, 0);
            transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);


        }
    }

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    location = target.transform.position;
    //    if(Player.transform.rotation.eulerAngles.y == 180)
    //    {
    //        location.z -= 1;
    //    }
    //    else
    //    {
    //        location.z += 1;
    //    }
    //    transform.position = location;
    //    transform.rotation = target.transform.rotation;
    //    //transform.LookAt(target.transform);

    //}
}
