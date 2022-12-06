using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public Rigidbody RB;
    public Animator AN;
    public SpriteRenderer SR;
    public bool Death;
    public Transform FierePos;
    public GameObject ScoreManager;
    public GameObject Rock; //돌멩이를 넣어줄 변수
    public Renderer HandRock;
    public PhotonView PV;
    public TMPro.TMP_Text NickNameText;
    public GameObject Player;
    public GameObject underWare;
    public float speed = 10.0f;
    public bool MyRockHave;
    bool isGround;
    bool SDown;
    public bool Win;
    bool jDown;
    bool RollingKey;
    bool isJump;
    bool Throwkey;
    bool isRolling;
    bool AttackDelay;
    bool RollingTimerSwitch;
    GameObject nearObject;
    public GameObject cinemachine;
    float Rollingtimer = 0.0f; // 구르기 재사용대기시간 측정 타이머
    float Deathtimer = 0.0f; // 죽음 타이머
    float AttackDelaytimer = 0.0f; //연사 금지
    public int score = 0;
    public int ScoreSyn = 0;
    Vector3 moveVec;
    Vector3 RollingVec;
    public Material[] playerMt;
    private Transform tr;
    private int idxMt = -1;
    void Awake()
    {
        //닉네임 설정
        NickNameText.text = photonView.Owner.NickName;
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; //자신의 닉네임과 상대방의 닉네임 구별
        NickNameText.color = PV.IsMine ? Color.black : Color.red; //자기 자신이면 블랙 상대일경우 레드


    }
    private void Start()
    {   //카메라 따라오기
        tr = GetComponent<Transform>();
        RB.gameObject.GetComponent<Rigidbody>();

        if (PV.IsMine)
        {
            cinemachine = GameObject.FindGameObjectWithTag("Follow");
            cinemachine.GetComponent<CinemachineFreeLook>().Follow= tr.Find("CamPivot").transform;
            cinemachine.GetComponent<CinemachineFreeLook>().LookAt = tr.Find("CamPivot").transform;
            Camera.main.GetComponent<LMS_Camera>().Player = GameObject.FindGameObjectWithTag("Player");
            Camera.main.GetComponent<LMS_Camera>().target = GameObject.Find("CamPviot");
        }
            
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
            Throwkey = Input.GetButtonDown("Fire1");
            Vector3 moveX, moveZ;

            moveX = transform.right * axis;
            moveZ = transform.forward * ver;

            moveVec = new Vector3(0, 0, ver)*speed;
            if(isRolling )
            {
                moveVec = RollingVec;
            }

            moveVec = (moveX + moveZ).normalized * speed;
            RB.velocity = moveVec;
            if (SDown && Death != true && AttackDelay != true)
            {
                RB.velocity = moveVec*1.2f;
                  
            }
            else if (Death != true && AttackDelay != true)
            {
                RB.velocity = moveVec;
                        
            }

            if (axis != 0 || ver != 0)
            {
                AN.SetBool("isRun", true);
                if (jDown && isJump)
                {
                    RB.AddForce(Vector3.up * 80, ForceMode.Impulse);
                    AN.SetBool("isJump", true);
                    AN.SetTrigger("doRunJump");
                    isJump = false;
                }

            }
            else AN.SetBool("isRun", false);

            AN.SetBool("isSprint", SDown);

            Jump();
            Rolling();
            if(RollingTimerSwitch == true)
            {
                Rollingtimer += Time.deltaTime;
            }

            if (Death == true)
            {
                Deathtimer += Time.deltaTime;
            }
            if (AttackDelay == true)
            {
                AttackDelaytimer += Time.deltaTime;
            }
            if(AttackDelaytimer >= 0.6f)
            {
                AttackDelay = false;
                AttackDelaytimer = 0.0f;
            }
            if (Deathtimer >= 1.3f) // 1초가  
            {
                CharDeath();
                Death = false;
                Rollingtimer = 0;
            }

            if (Rollingtimer >= 3) // 4초가  
            {
                RollingTimerSwitch = false;
                Rollingtimer = 0;
            }

            if (Input.GetButtonDown("Fire1")&& Death!=true && AttackDelay != true &&MyRockHave) // Death 불값이 true가 아니면 Fire1 버튼을 누를때 실행됨
            {
                GameObject ins =PhotonNetwork.Instantiate("Rock", FierePos.transform.position, FierePos.transform.rotation) as GameObject;
                AN.SetTrigger("doThrow");
                AttackDelay = true;
                MyRockHave = false;
                NotHaveRock();
            }

           
        }
    }


    private void OnCollisionEnter(Collision collision) //일반충돌이 생기면 호출 , 매개변수에 충돌한 개체에 정보가 넘어온다
    {
        if(collision.gameObject.tag == "Floor")
        {
            AN.SetBool("isJump", false);
            isJump = true;
        }

        string coll = collision.gameObject.name;
        switch (coll)
        {
            case "Item_1":
                idxMt = 0;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 0);
                break;
            case "Item_2":
                idxMt = 1;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 1);
                break;
            case "Item_3":
                idxMt = 2;
                PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, 2);
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ItemRock")
        {
            HandRock.enabled = true;
            MyRockHave = true;
        }
    }

    //점프 메소드
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && isJump && !isRolling && Death != true && AttackDelay != true)
        {
            RB.AddForce(Vector3.up * 80, ForceMode.Impulse);
            AN.SetBool("isJump", true);
            AN.SetTrigger("doJump");
            isJump = false;
        }
    }
    //구르기 메소드
    void Rolling()
    {
        if (RollingKey && moveVec !=Vector3.zero && !isRolling &&!RollingTimerSwitch && Death != true && AttackDelay != true)
        {
            RollingVec = moveVec*2f;
            
            AN.SetTrigger("doRolling");
            isRolling = true;

            Invoke("RollingOut", 0.6f);
        }
    }

    void DeathEvent()
    {
        AN.SetTrigger("doDeath");
        Death = true;
    }
    //구르기시 속도와 체크
    void RollingOut()
    {
        isRolling = false;
        RollingTimerSwitch = true;
    }

    public void InitColor(int num)
    {
        PV.RPC(nameof(SetMt), RpcTarget.AllViaServer, num);
    }

    [PunRPC]
    private void SetMt(int idx)
    {
        underWare.GetComponent<Renderer>().material = playerMt[idx];
        idxMt = idx;
    }

    public void Hit() //돌멩이에 맞았을때
    {   
            DeathEvent();
        if (GetComponent<TFGunWinner>().PlaysCount == 4)
        {
            score = 4;
        }
        else if (GetComponent<TFGunWinner>().PlaysCount == 3)
        {
            score = 6;
        }
        else if (GetComponent<TFGunWinner>().PlaysCount == 2)
        {
            score = 8;
        }
        else if (GetComponent<TFGunWinner>().PlaysCount == 1)
        {
            score = 10;
        }
        GetComponent<TFGunWinner>().PlaysCount--;
    }

    void CharDeath()
    {
        PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }
    void NotHaveRock()
    {
        PV.RPC("NotHaveRockRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void NotHaveRockRPC()
    {
        HandRock.enabled = false;
    }

    [PunRPC]
    void DestroyRPC()
    {
        Destroy(gameObject);
    } 

    public override void OnPlayerEnteredRoom(Player newPlayer) //방에 들어오기전 상대플레이어가 색을 바꿨을경우 동기화 메소드
    {
        if(PV.IsMine && idxMt != -1)
        {
            PV.RPC(nameof(SetMt), newPlayer, idxMt);
        }
    }
}
