using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    float lookSpeed = 10f;

    Vector3 bossStartPos;

    enum SkillState
    {
        None,
        Throw,
        Spray,
    }

    SkillState skillState = SkillState.None;

    protected override void Init()
    {
        base.Init();
        state = EnemyState.Move;
        bossStartPos = transform.position;
        maxHp = 100;
    }

    protected override void Loop()
    {
        base.Loop();
        //Debug.Log("BossHP : " + hp);
    }

    //protected override void Idle()
    //{
    //    base.Idle();
    //
    //    if(Vector3.Distance(transform.position,target.transform.position)<findRange)
    //    {
    //        //this.transform.LookAt(target.transform);
    //
    //        Vector3 forward = target.transform.position - transform.position;
    //        Quaternion quaternion = Quaternion.LookRotation(forward);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * lookSpeed);
    //
    //        if (Vector3.Distance(transform.position, target.transform.position)< moveRange)
    //        {
    //            state = EnemyState.Move;
    //        }
    //    }
    //}

    protected override void Move()
    {
        base.Move();
        nav.SetDestination(target.transform.position);

        if(Vector3.Distance(transform.position,target.transform.position) < attackRange)
        {
            state = EnemyState.Attack;
        }

        //if(Vector3.Distance(transform.position,target.transform.position) > moveRange)
        //{
        //    state = EnemyState.Return;
        //}
    }

    protected override void Attack()
    {
        transform.LookAt(target.transform);
        base.Attack();

        int random = Random.Range(0, 2);

        if(Vector3.Distance(transform.position,target.transform.position)>attackRange)
        {
            switch(random)
            {
                //추적
                case 0:
                    skillState = SkillState.None;
                    state = EnemyState.Move;
                    break;
                //눈던지기
                case 1:
                    state = EnemyState.None;
                    skillState = SkillState.Throw;
                    Throw();
                    break;
                //눈뿌리기
                case 2:
                    state = EnemyState.None;
                    skillState = SkillState.Spray;
                    break;
            }
            
        }
    }

    //protected override void Return()
    //{
    //    base.Return();
    //    nav.SetDestination(bossStartPos);
    //    if (Vector3.Distance(bossStartPos, transform.position) < 0.2f || Vector3.Distance(this.transform.position, target.transform.position) < attackRange)
    //    {
    //        Debug.Log("~!");
    //        state = EnemyState.None;
    //    }  
    //}

    public override void DecreaseHP(int attackDamage)
    {
        base.DecreaseHP(attackDamage);
    }

    protected override void Die()
    {
        base.Die();
    }

    //스킬 
    void Throw()
    {
        transform.forward = target.transform.position - transform.position;
        animator.SetTrigger("Throw");
    }
}
