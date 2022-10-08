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
    bool RollingKey;
    bool isJump;
    bool isRolling;
    Vector3 moveVec;
    Vector3 RollingVec;
    private Transform tr;
    void Awake()
    {
        //�г��� ����
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName; //�ڽ��� �г��Ӱ� ������ �г��� ����
        NickNameText.color = PV.IsMine ? Color.black : Color.red; //�ڱ� �ڽ��̸� �� ����ϰ�� ����

    }
    private void Start()
    {   //ī�޶� �������
        tr = GetComponent<Transform>(); 
        if (PV.IsMine)
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
    }
    void Update()
    {   
        if (PV.IsMine) //�ڽ��ϰ��
        {
            // �÷��̾� �̵�
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

            //�г��� �� ���� ȸ��
            float yxis = Player.transform.eulerAngles.y;
            if (yxis == 90)
            {
                NickNameText.transform.eulerAngles = new Vector3(0, -yxis, 0);
            }

            //�ٴ�üũ
            jump();
            Rolling();
        }
    }


    private void OnCollisionEnter(Collision collision) //�Ϲ��浹�� ����� ȣ�� , �Ű������� �浹�� ��ü�� ������ �Ѿ�´�
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = true;
        }
    }

    void jump()
    {
        if (jDown && moveVec == Vector3.zero && isJump && !isRolling)
        {
            RB.AddForce(Vector3.up * 5, ForceMode.Impulse);
            AN.SetBool("isJump", true);
            AN.SetTrigger("doJump");
            isJump = false;
        }
    }

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

    void RollingOut()
    {
        speed *= 0.5f;
        isRolling = false;
    }
}
