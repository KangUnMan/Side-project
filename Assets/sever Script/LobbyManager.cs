using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviour
{

    private PhotonView pv;

    private Hashtable CP; //해쉬 테이블

    public Material[] playerMt; // 매테리얼 값을 저장할 배열

    private Ray ray; //레이를 날려서

    private new Camera camera;

    private RaycastHit hit; //레이의 충돌이 일어났는지 아닌지 감지

    public Renderer UnderWare; // 플레이어 개체

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        camera = Camera.main;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Color", -1 }}); // 키 ,값으로 저장  찾을때 키값주면 됨 , 제일 빠르다고 한다.
        CP = PhotonNetwork.LocalPlayer.CustomProperties;
    }

    // Update is called once per frame
    private void Update()
    {   
        ray = camera.ScreenPointToRay(Input.mousePosition); // 레이를 클릭한 포인트에
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit , 1<<6))
            {
                string item = hit.collider.gameObject.name;
                string[] words = item.Split('_');          // 문제없음
                SetColorProperty(int.Parse(words[1]));
            }
        }
    }

    public void SetColorProperty(int num) 
    {
        CP["Color"] = num;
        SetMt(num);
    }

    void SetMt(int num)
    {
        UnderWare.material = playerMt[num -1];
    }
}
