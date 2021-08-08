using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : Obstacles
{
    Rigidbody rigid;
    bool isJump;

    protected override void Init()
    {
        base.Init();
        ot = ObstaclesType.BounceBall;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Bounce();
    }

    void Bounce()
    {
        if(!isJump)
        {
            float value = Random.Range(6.0f, 9.0f);
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * value, ForceMode.Impulse);
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Ground")
        {
            isJump = false;
        }

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 12.0f,ForceMode.Impulse);
        }
    }
}
