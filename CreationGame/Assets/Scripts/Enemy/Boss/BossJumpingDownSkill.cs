using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpingDownSkill : MonoBehaviour
{
    [SerializeField]
    Boss boss;

    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JumpingDown();
    }

    public void JumpingDown()
    {
        if (boss.skillState != Boss.SkillState.JumpingDown)
            return;

        animator.SetTrigger("JumpingDown");

        StartCoroutine("testCor");
    }

    IEnumerator testCor()
    {
        yield return new WaitForSeconds(2.0f);
        boss.skillState = Boss.SkillState.None;
        boss.state = Boss.EnemyState.Idle;
    }
}
