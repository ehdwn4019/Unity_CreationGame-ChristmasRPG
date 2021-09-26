using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : Obstacles
{
    [SerializeField]
    ParticleSystem coll;

    Rigidbody rigid;

    [SerializeField]
    float speed;

    bool isJump;

    protected override void Init()
    {
        base.Init();
        ot = ObstaclesType.BounceBall;
        rigid = GetComponent<Rigidbody>();
    }

    protected override void FixedLoop()
    {
        base.FixedLoop();
        Bounce();
    }

    void Bounce()
    {
        if(!isJump)
        {
            coll.Play();
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
            float x = Random.Range(-1, 1);
            float z = 0;
            
            if (x == 0)
            {
                z = -1;
            }

            SoundManager.instance.PlaySoundEffect("공맞았을때");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x,0f,z) * speed, ForceMode.Impulse);
        }
    }
}
