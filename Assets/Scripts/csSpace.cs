using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSpace : MonoBehaviour
{
    public int speed = 10; // �����̴� �ӵ�
    float ver; //    Axis(Vertical) �� ���� ���� �������� ����
    float ang; //    Axis(Horizontal) �� ���� ���� �������� ����
    bool wDown;
    Vector3 moveVec; // Vertical�� Horizontal�� ���� ��ĥ moveVec ����

    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>(); //�ڽ� ������Ʈ�� �ִ� ���� ������ Animator ������ �ʱ�ȭ
    }

    // Update is called once per frame
    void Update() //�����Ӹ��� ȣ���
    {

        ver = Input.GetAxis("Vertical"); //  Vertical�� ���� ����  GetAxis()�� axis�� ������ ��ȯ�ϴ� �Լ�
        ang = Input.GetAxis("Horizontal"); //Horizontal �� ����
        //wDown = Input.GetButton("Walk"); //shift�� �������� �۵��ǵ��� GETButton() �Լ����

        //moveVec = new Vector3(ang, 0, ver).normalized; // (noranlized ��� ������ ���� 1�� ��������)
        //transform.position += moveVec * speed * Time.deltaTime; // �����ǿ� ���� �� �����ְ� ������ ����Ʈ�� ������� ������ �ӵ��� ����
        //��Ű�� ���� Time.deltaTime�� �����ش�.

        //anim.SetBool("isRun", moveVec != Vector3.zero); // setbool �Լ��� �Ķ���� ���� �����ϱ�
        //anim.SetBool("isWalk", wDown); 
    }
}
