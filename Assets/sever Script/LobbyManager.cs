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

    private Hashtable CP; //�ؽ� ���̺�

    public Material[] playerMt; // ���׸��� ���� ������ �迭

    private Ray ray; //���̸� ������

    private new Camera camera;

    private RaycastHit hit; //������ �浹�� �Ͼ���� �ƴ��� ����

    public Renderer UnderWare; // �÷��̾� ��ü

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        camera = Camera.main;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Color", -1 } }); // Ű ,������ ����  ã���� Ű���ָ� �� , ���� �����ٰ� �Ѵ�.
        CP = PhotonNetwork.LocalPlayer.CustomProperties;
    }

    // Update is called once per frame
    private void Update()
    {   
        ray = camera.ScreenPointToRay(Input.mousePosition); // ���̸� Ŭ���� ����Ʈ��
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit , 1<<6))
            {
                string item = hit.collider.gameObject.name;
                string[] words = item.Split('_');          // ��������
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
