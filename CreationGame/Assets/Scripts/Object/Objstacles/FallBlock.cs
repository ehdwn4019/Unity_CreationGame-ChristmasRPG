using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : Obstacles
{
    Vector3 startPos;
    Quaternion startRotation;
    Rigidbody rigid;
    bool isFalling;

    private void Awake()
    {
        ot = ObstaclesType.FallBlock;
        startPos = transform.position;
        startRotation = transform.rotation;
    }
    
    private void OnEnable()
    {
        if(rigid == null)
            rigid = gameObject.AddComponent<Rigidbody>();

        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        transform.position = startPos;
        gameObject.transform.rotation = startRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            rigid.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "ResponeZone")
        {
            FallBlockSpawn.instance.Disappear(gameObject);

            //Rigidbody 삭제
            Destroy(rigid);

            Invoke("ReturnPos", 3.0f);
        }
    }

    //Corutine에서는 gameObject가 비활성화되면 실행x , 대신 invoke함수 사용 
    void ReturnPos()
    {
        FallBlockSpawn.instance.Appear(startPos);
    }
}