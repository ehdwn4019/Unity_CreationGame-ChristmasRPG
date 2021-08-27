using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Vector3 bossStartPos;
    [SerializeField] float lookSpeed = 10f;

    protected override void Init()
    {
        base.Init();
        state = EnemyState.Idle;
        bossStartPos = transform.position;
        maxHp = 100;
    }

    protected override void Loop()
    {
        base.Loop();
        //Debug.Log("BossHP : " + hp);
    }

    protected override void Idle()
    {
        base.Idle();

        if(Vector3.Distance(transform.position,target.transform.position)<findRange)
        {
            //this.transform.LookAt(target.transform);

            Vector3 forward = target.transform.position - transform.position;
            Quaternion quaternion = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * lookSpeed);

            if (Vector3.Distance(transform.position, target.transform.position)< moveRange)
            {
                state = EnemyState.Move;
            }
            
        }
    }

    protected override void Move()
    {
        base.Move();
        nav.SetDestination(target.transform.position);

        if(Vector3.Distance(transform.position,target.transform.position) < attackRange)
        {
            state = EnemyState.Attack;
        }

        if(Vector3.Distance(transform.position,target.transform.position) > moveRange)
        {
            state = EnemyState.Return;
        }
    }

    protected override void Attack()
    {
        transform.LookAt(target.transform);
        base.Attack();

        if(Vector3.Distance(transform.position,target.transform.position)>attackRange)
        {
            state = EnemyState.Move;
        }
    }

    protected override void Return()
    {
        base.Return();
        nav.SetDestination(bossStartPos);
        if(Vector3.Distance(bossStartPos,transform.position)<0.2f || Vector3.Distance(this.transform.position, target.transform.position) < attackRange)
        {
            Debug.Log("~!");
            state = EnemyState.Idle;
        }

        
    }

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
        animator.SetTrigger("Throw");
    }
}
