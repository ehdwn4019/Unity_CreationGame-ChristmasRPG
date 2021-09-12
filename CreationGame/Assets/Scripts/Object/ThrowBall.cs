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

    private void Awake()
    {
        boss = GameObject.Find("Boss");
        throwPos = GameObject.Find("ThrowPos");
        throwSkill = FindObjectOfType<BossThrowSkill>();
        startPos = throwPos.transform.position;
        startRotation = throwPos.transform.rotation;
        //startPos = transform.position;
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
        if (throwSkill.IsThrowBallSkill)
            isThrowBall = true;
    }

    // Update is called once per frame
    void Update()
    {
        Throw();
        //transform.forward = boss.transform.forward;
    }

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
            ThrowBallSpawn.instance.Disappear(gameObject);
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1.5f);
        transform.Translate(transform.right* 8.0f * Time.deltaTime);
        Debug.Log(boss.transform.forward);
    }
}
