using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMFreeLookSetting : MonoBehaviour
{
    CinemachineFreeLook freeLook;
    public float scrollSpeed = 2000.0f;
    void Awake()
    {
        CinemachineCore.GetInputAxis = clickControl;
    }

    void Start()
    {
        freeLook = this.GetComponent<CinemachineFreeLook>();
    }

    public float clickControl(string axis)
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        freeLook.m_Lens.FieldOfView += scrollWheel * Time.deltaTime * scrollSpeed;

        return 0;
    }
}