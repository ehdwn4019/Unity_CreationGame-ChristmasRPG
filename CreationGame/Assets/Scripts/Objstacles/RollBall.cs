using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBall : Obstacles
{
    [SerializeField]
    float power;

    Vector3 startPos;

    private void Awake()
    {
        ot = ObstaclesType.RollBall;
        startPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = startPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name =="Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * power, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RollBallStopZone")
        {
            RollBallSpawn.instance.Disappear(gameObject);
            //gameObject.SetActive(false);
            Invoke("ReturnPos", 1.0f);
        }
    }

    //Corutine에서는 gameObject가 비활성화되면 실행x , 대신 invoke함수 사용
    void ReturnPos()
    {
        RollBallSpawn.instance.Appear(startPos);
        //gameObject.SetActive(true);
    }
}
