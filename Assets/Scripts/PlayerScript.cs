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
    public GameObject underWare;
    public float speed = 10.0f;
    bool isGround;
    bool SDown;
    bool jDown;
    bool RollingKey;
    bool isJump;
    bool isRolling;
    Vector3 moveVec;
    Vector3 RollingVec;
    public Material[] playerMt;
    private Transform tr;
    private int idxMt = -1;
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
            RollingKey = Input.GetButtonDown("Rolling");

            moveVec = new Vector3(axis, 0, ver).normalized;
            if(isRolling)
            {
                moveVec = RollingVec;
            }

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
                if (jDown && isJump)
                {
                    RB.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    AN.SetBool("isJump", true);
                    AN.SetTrigger("doRunJump");
                    isJump = false;
                }

            }
            else AN.SetBool("isRun", false);

            AN.SetBool("isSprint", SDown);

            Jump();
            Rolling();
        }
    }


    private void OnCollisionEnter(Collision collision) //일반충돌이 생기면 호출 , 매개변수에 충돌한 개체에 정보가 넘어온다
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = true;
        }
        string coll = collision.gameObject.name;
        switch (coll)
        {
            case "item_1":
                idxMt = 0;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 0);
                break;
            case "item_2":
                idxMt = 1;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 1);
                break;
            case "item_3":
                idxMt = 2;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 2);
                break;

        }
    }
    //점프 메소드
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && isJump && !isRolling)
        {
            RB.AddForce(Vector3.up * 5, ForceMode.Impulse);
            AN.SetBool("isJump", true);
            AN.SetTrigger("doJump");
            isJump = false;
        }
    }
    //구르기 메소드
    void Rolling()
    {
        if (RollingKey && moveVec !=Vector3.zero && !isJump && !isRolling)
        {
            RollingVec = moveVec;
            speed *= 2;
            AN.SetTrigger("doRolling");
            isRolling = true;
            isJump = false;

            Invoke("RollingOut", 0.6f);
        }
    }
    //구르기시 속도와 체크
    void RollingOut()
    {
        speed *= 0.5f;
        isRolling = false;
    }



    [PunRPC]
    private void SetMt(int idx)
    {
        underWare.GetComponent<Renderer>().material = playerMt[idx];
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //방에 들어오기전 상대플레이어가 색을 바꿨을경우 동기화 메소드
    {
        if(PV.IsMine && idxMt != -1)
        {
            PV.RPC(nameof(SetMt), newPlayer, idxMt);
        }
    }
}
