using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Utility;
public class PlayerScript : MonoBehaviourPunCallbacks
{
    public Rigidbody RB;
    public Animator AN;
    public SpriteRenderer SR;
    public PhotonView PV;
    public Text NickNameText;
    public GameObject Player;
    public float speed = 10.0f;
    bool isGround;
    bool SDown;
    bool jDown;
    bool isJump;
    Vector3 moveVec;
    private Transform tr;
    void Awake()
    {
        //닉네임 설정
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; //자신의 닉네임과 상대방의 닉네임 구별
        NickNameText.color = PV.IsMine ? Color.black : Color.red; //자기 자신이면 블랙 상대일경우 레드

    }
    private void Start()
    {   //카메라 따라오기
        tr = GetComponent<Transform>(); 
        if (PV.IsMine)
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
    }
    void Update()
    {   
        if (PV.IsMine) //자신일경우
        {
            // 플레이어 이동
            float axis = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            SDown = Input.GetButton("Sprint");
            jDown = Input.GetButtonDown("Jump");


            moveVec = new Vector3(axis, 0, ver).normalized;

            if (SDown)
            {
                transform.position += moveVec * speed * 1.5f * Time.deltaTime;
                transform.LookAt(transform.position + moveVec);      
            }
            else
            {
                transform.position += moveVec * speed * Time.deltaTime;
                transform.LookAt(transform.position + moveVec);         
            }

            if (axis != 0 || ver != 0)
            {
                AN.SetBool("isRun", true);
                
            }
            else AN.SetBool("isRun", false);

            AN.SetBool("isSprint", SDown);

            //닉네임 바 같이 회전
            float yxis = Player.transform.eulerAngles.y;
            if (yxis == 90)
            {
                NickNameText.transform.eulerAngles = new Vector3(0, -yxis, 0);
            }

            //바닥체크

            if (jDown && isJump)
            {
                RB.AddForce(Vector3.up * 5, ForceMode.Impulse);
                AN.SetBool("isJump", true);
                AN.SetTrigger("doJump");
                isJump = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision) //일반충돌이 생기면 호출 , 매개변수에 충돌한 개체에 정보가 넘어온다
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = true;
        }
    }

}
