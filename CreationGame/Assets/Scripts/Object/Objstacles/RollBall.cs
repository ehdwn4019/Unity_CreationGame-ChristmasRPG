using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : Obstacles
{
    [SerializeField]
    float speed;

    GameObject startPos;
    float x;
    float z;

    private void Awake()
    {
        ot = ObstaclesType.RollBall;
        startPos = GameObject.Find("RoleBallPos");
    }

    private void OnEnable()
    {
        transform.rotation = startPos.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name =="Player")
        {
            float x = Random.Range(-1, 1);
            float z = 0;

            if (x == 0)
            {
                z = -1;
            }

            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x,0f,z) * speed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RollBallStopZone" || other.gameObject.name == "ResponeZone")
        {
            RollBallSpawn.instance.Disappear(gameObject);
            Invoke("ReturnPos", 1.0f);
        }
    }

    //Corutine에서는 gameObject가 비활성화되면 실행x , 대신 invoke함수 사용
    void ReturnPos()
    {
        RollBallSpawn.instance.Appear(startPos.transform.position);
    }
}
