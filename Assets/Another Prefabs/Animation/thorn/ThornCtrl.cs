using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornCtrl : MonoBehaviour
{
    private Animator anim;
    public int Num = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("ThornNum", Num);
    }
}
