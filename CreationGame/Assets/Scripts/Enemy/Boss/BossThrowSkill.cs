using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowSkill : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject throwPos;

    [SerializeField]
    float speed = 10.0f;

    Boss boss;
    Animator animator;
    ThrowBall throwBall;
    GameObject go;
    Vector3 dir;
    float x;

    GameObject[] gos = new GameObject[3];

    //List<GameObject> ball = new List<GameObject>();
    //GameObject[] ball = new GameObject[3];
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
        //go.transform.position = rightHand.transform.position;
        //CreateBall();
        
    }


    private void FixedUpdate()
    {
        //ThrowBall();
    }

    public void Throw()
    {
        //boss.nav.ResetPath();
        animator.SetBool("Throw",true);
    }

    public void CreateBall()
    {
        //for(int i=0; i<ballCount; i++)
        //{
        //    go = ThrowBallSpawn.instance.Appear(rightHand.transform.position);
        //    
        //    //ball.Add(ThrowBallSpawn.instance.Appear(throwPos.transform.position));
        //}

        go = ThrowBallSpawn.instance.Appear(throwPos.transform.position);
        isFollowBall = true;
    }

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
        //isFollowBall = false;
        //throwBall.Throw();
        //isThrowBallSkill = true;
        //throwBall.Throw();

        dir = target.transform.position - transform.position;

        if (go != null && !isFollowBall)
            go.transform.Translate(dir * speed * Time.deltaTime);
            

        //StartCoroutine("ThrowDelay");
            //go.transform.Translate(dir * 10.0f *Time.deltaTime);
        //go.GetComponent<Rigidbody>().AddForce(dir * 10.0f);

        
        //dir = boss.transform.forward;
        //for(int i=0; i<ballCount; i++)
        //{
        //    //ball[i].transform.Translate(boss.transform.forward * 8.0f *Time.deltaTime);
        //    ball[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * 20.0f,ForceMode.Impulse);
        //}
    }

    public void End()
    {
        animator.SetBool("Throw", false);
        boss.bossState = Boss.BossState.Idle;
    }
}
