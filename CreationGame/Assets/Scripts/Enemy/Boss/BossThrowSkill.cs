using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowSkill : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject throwPos;

    Boss boss;
    Animator animator;
    ThrowBall throwBall;
    GameObject go;
    Vector3 dir;
    GameObject[] gos = new GameObject[3];

    [SerializeField]
    float speed = 10.0f;

    float x;
    float ballCount = 3;
    bool isFollowBall;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
        animator = GetComponent<Animator>();
        throwBall = FindObjectOfType<ThrowBall>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowBall();
        ThrowBall();
    }

    private void FixedUpdate()
    {
        //ThrowBall();
    }

    public void Throw()
    {
        animator.SetBool("Throw",true);
    }

    public void CreateBall()
    {
        go = ThrowBallSpawn.instance.Appear(throwPos.transform.position);
        isFollowBall = true;
    }

    //애니메이션에 맞춰 공이 보스 손에 따라가기
    public void FollowBall()
    {
        if(isFollowBall && go != null)
            go.transform.position = throwPos.transform.position;
    }

    public void NotFollowBall()
    {
        isFollowBall = false;
    }

    public void ThrowBall()
    {
        if (go != null && !isFollowBall)
        {
            dir = target.transform.position - transform.position;
            go.transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    public void End()
    {
        animator.SetBool("Throw", false);
        boss.bossState = Boss.BossState.Idle;
    }
}
