using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTestVO : MonoBehaviour
    // 플레이어 스크립트에 들어가야 할 내용 예시
{
    
    public string UID;
    public string score;

    private void Awake()
    {
    }
    void Start()
    {
        UID = "Hwan";   // 임시로 넣음 // 로그인 화면에서 텍스트 받아오기
        score = DBManager.getScore(UID);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
