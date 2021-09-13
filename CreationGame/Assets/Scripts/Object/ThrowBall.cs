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

        Debug.Log(startPos);
        Debug.Log(startRotation);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    private void OnEnable()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
        //if (throwSkill.isThrowBallSkill)
        //    isThrowBall = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Throw();
        //transform.forward = boss.transform.forward;
    }

    //private void FixedUpdate()
    //{
    //    Throw();
    //}

    public void Throw()
    {
        if (isThrowBall)
            StartCoroutine("ThrowDelay");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "IceBallStopZone")
        {
            isThrowBall = false;
            //throwSkill.isThrowBallSkill = false;
            ThrowBallSpawn.instance.Disappear(gameObject);
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1.5f);
        transform.Translate(transform.right* 8.0f * Time.deltaTime);
        //rigid.AddForce(boss.transform.forward * 8.0f);
        Debug.Log(boss.transform.forward);
    }
}
