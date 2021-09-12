using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowSkill : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject throwPos;

    //[SerializeField]
    Boss boss;

    //[SerializeField]
    Animator animator;

    GameObject go;
    

    //ThrowBall throwBall;

    //List<GameObject> ball = new List<GameObject>();
    GameObject[] ball = new GameObject[3];
    float ballCount = 3;

    bool isFollowBall;
    bool isThrowBallSkill;
    Vector3 dir;

    public bool IsThrowBallSkill { get { return isThrowBallSkill; } }
    public Vector3 Dir { get { return dir; } }
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
        animator = GetComponent<Animator>();
        //throwBall = FindObjectOfType<ThrowBall>();
    }

    // Update is called once per frame
    void Update()
    {
        //ThrowBall();
        //go.transform.position = rightHand.transform.position;
        //CreateBall();
        FollowBall();
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

    public void ThrowBall()
    {
        isFollowBall = false;
        //throwBall.Throw();
        isThrowBallSkill = true;

        
        //dir = boss.transform.forward;
        //for(int i=0; i<ballCount; i++)
        //{
        //    //ball[i].transform.Translate(boss.transform.forward * 8.0f *Time.deltaTime);
        //    ball[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * 20.0f,ForceMode.Impulse);
        //}
    }

    //public void GetDir()
    //{
    //    dir = boss.transform.position - target.transform.position;
    //    dir.y = 0f;
    //    Debug.Log(dir);
    //}

    public void End()
    {
        //animator.ResetTrigger("Throw");
        //boss.skillState = Boss.SkillState.None;
        animator.SetBool("Throw", false);
        boss.bossState = Boss.BossState.Idle;
    }
}
