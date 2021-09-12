using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpingDownSkill : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;

    //[SerializeField]
    Boss boss;

    //[SerializeField]
    Animator animator;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //JumpingDown();
    }

    public void JumpingDown()
    {
        animator.SetTrigger("JumpingDown");

        //StartCoroutine("testCor");
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * speed * Time.deltaTime);
       
    }

    public void LandGround()
    {
        //animator.ResetTrigger("JumpingDown");
        //boss.skillState = Boss.SkillState.None;
        boss.bossState = Boss.BossState.Idle;
    }

    //IEnumerator testCor()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    boss.skillState = Boss.SkillState.None;
    //    boss.state = Boss.EnemyState.Idle;
    //}
}
