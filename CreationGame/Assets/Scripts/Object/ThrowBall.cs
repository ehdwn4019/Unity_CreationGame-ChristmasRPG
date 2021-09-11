using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    GameObject boss;
    BossThrowSkill throwSkill;
    Vector3 startPos;
    float angle = 360 / ThrowBallSpawn.instance.ballCount;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        throwSkill = FindObjectOfType<BossThrowSkill>();
        startPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        //Throw();
    }

    public void Throw()
    {
        //if (throwSkill.IsThrowBall)
            transform.Translate(Vector3.back * 8.0f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "IceBallStopZone")
        {
            ThrowBallSpawn.instance.Disappear(gameObject);
        }
    }
}
