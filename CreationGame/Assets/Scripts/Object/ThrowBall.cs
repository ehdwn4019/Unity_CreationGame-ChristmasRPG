using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    GameObject boss;
    GameObject throwPos;
    BossThrowSkill throwSkill;
    Vector3 startPos;
    Quaternion startRotation;
    bool isThrowBall;
    float speed = 8.0f;
    //float angle = 360 / ThrowBallSpawn.instance.ballCount;

    Rigidbody rigid;

    private void Awake()
    {
        boss = GameObject.Find("Boss");
        throwPos = GameObject.Find("ThrowPos");
        throwSkill = FindObjectOfType<BossThrowSkill>();
        startPos = transform.position;
        startRotation = transform.rotation;
        //startPos = transform.position;

        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        transform.position = startPos;
        transform.rotation = startRotation;
        //if (throwSkill.isThrowBallSkill)
        //    isThrowBall = true;
    }

    public void Throw()
    {
        if (isThrowBall)
            StartCoroutine("ThrowDelay");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.name == "Player" || collision.gameObject.name == "IceBallStopZone")
    //    {
    //        isThrowBall = false;
    //        //rigid.velocity = Vector3.zero;
    //        //speed = 0f;
    //        //throwSkill.isThrowBallSkill = false;
    //        ThrowBallSpawn.instance.Disappear(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "IceBallStopZone")
        {
            isThrowBall = false;
            //rigid.velocity = Vector3.zero;
            //speed = 0f;
            //throwSkill.isThrowBallSkill = false;
            ThrowBallSpawn.instance.Disappear(gameObject);
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1.5f);
        //transform.Translate(transform.right* speed * Time.deltaTime);
        rigid.AddForce(transform.right  * speed);
        Debug.Log(boss.transform.forward);
    }
}
