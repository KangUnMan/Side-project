using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMS_Monster_Chase : MonoBehaviour
{
    public GameObject target;
    public GameObject explosion;
    float speed, timer;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f * Time.deltaTime;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        if(timer >= 0.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            speed += 0.001f * Time.deltaTime;
            timer = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
