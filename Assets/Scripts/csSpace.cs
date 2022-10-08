using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSpace : MonoBehaviour
{
    public int speed = 10; // 움직이는 속도
    float ver; //    Axis(Vertical) 의 값을 받을 전역변수 선언
    float ang; //    Axis(Horizontal) 의 값을 받을 전역변수 선언
    bool wDown;
    Vector3 moveVec; // Vertical과 Horizontal의 값을 합칠 moveVec 선언

    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>(); //자식 컴포넌트에 있는 것을 가져옴 Animator 변수를 초기화
    }

    // Update is called once per frame
    void Update() //프레임마다 호출됨
    {

        ver = Input.GetAxis("Vertical"); //  Vertical의 값을 대입  GetAxis()는 axis를 정수로 반환하는 함수
        ang = Input.GetAxis("Horizontal"); //Horizontal 값 대입
        //wDown = Input.GetButton("Walk"); //shift는 누를때만 작동되도록 GETButton() 함수사용

        //moveVec = new Vector3(ang, 0, ver).normalized; // (noranlized 모든 방향의 값을 1로 보정해줌)
        //transform.position += moveVec * speed * Time.deltaTime; // 포지션에 위에 값 더해주고 프레임 레이트와 상관없이 일정한 속도로 진행
        //시키기 위해 Time.deltaTime를 곱해준다.

        //anim.SetBool("isRun", moveVec != Vector3.zero); // setbool 함수로 파라메터 값을 설정하기
        //anim.SetBool("isWalk", wDown); 
    }
}
