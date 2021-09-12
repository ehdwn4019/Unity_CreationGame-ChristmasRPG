using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    [SerializeField]
    float lookSpeed = 10f;

    [SerializeField]
    Slider slider;

    [SerializeField]
    Text text;

    [SerializeField]
    GameObject IceBallStopZone;

    [SerializeField]
    GameObject stormCloud;

    [SerializeField]
    SphereCollider sphereCollider;

    [SerializeField]
    BossThrowSkill throwSkill;

    [SerializeField]
    BossJumpingDownSkill jumpingDownSkill;

    Vector3 startPos;
    float attackDelay = 1.0f;
    //float skillDelay = 2.0f;

    //public enum SkillState
    //{
    //    None,
    //    Throw,
    //    JumpingDown,
    //}
    //
    //public SkillState skillState = SkillState.None;

    protected override void Init()
    {
        base.Init();
        bossState = BossState.Idle;
        startPos = transform.position;
        maxHp = 100;
        currentHp = maxHp;
    }

    protected override void Loop()
    {
        base.Loop();
        Forward();
        AppearHPUI();
        Debug.Log("state : "+ bossState);
        //Debug.Log("skillstate : "+skillState);
        //Debug.Log(GameManager.instance.isFightBoss);
    }

    protected override void Idle()
    {
        //animator.ResetTrigger("Throw");
        //animator.ResetTrigger("JumpingDown");

        //플레이어 죽었을때 idle 상태로 돌리기 
        base.Idle();
        nav.ResetPath();

        if (!GameManager.instance.isFightBoss)
            return;

        //skillDelay -= Time.deltaTime;
        //Debug.Log(skillDelay);

        

        //Debug.Log(random);

        if (Vector3.Distance(transform.position, target.transform.position) < moveRange)
        {
            //skillDelay = 4.0f;
            bossState = BossState.Move;
            //skillState = SkillState.None;
        }
        else
        {
            //skillDelay = 4.0f;
        
            //Invoke로 랜덤값 조정하기 
            int random = Random.Range(0, 2);
        
            switch (0)
            {
                //눈던지기
                case 0:
                    //bossState = BossState.None;
                    //skillState = SkillState.Throw;
                    bossState = BossState.FirstSkill;
                    throwSkill.Throw();
                    break;
                //점프내려찍기
                //case 1:
                //    state = EnemyState.None;
                //    skillState = SkillState.JumpingDown;
                //    jumpingDownSkill.JumpingDown();
                //    break;
                //    ////눈뿌리기
                //    //case 2:
                //    //    state = EnemyState.None;
                //    //    skillState = SkillState.Spray;
                //    //    break;
            }
        
        }
    }

    protected override void Move()
    {
        //animator.ResetTrigger("Throw");
        //animator.ResetTrigger("JumpingDown");

        base.Move();
        nav.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            bossState = BossState.Attack;
            //skillState = SkillState.None;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > moveRange)
        {
            bossState = BossState.Idle;
            //skillState = SkillState.None;
        }
        
        //else if(Vector3.Distance(transform.position,target.transform.position)>attackRange && Vector3.Distance(transform.position, target.transform.position)<findRange)
        //{
        //    state = EnemyState.Move;
        //    skillState = SkillState.None;
        //}
    }

    protected override void Attack()
    {
        //animator.ResetTrigger("Throw");
        //animator.ResetTrigger("JumpingDown");

        base.Attack();

        int attackDamage = Random.Range(8, 10);

        attackDelay -= Time.deltaTime;

        if(attackDelay<=0f)
        {
            if (target != null)
                target.GetComponent<Player>().DecreaseHP(attackDamage);

            attackDelay = 1.0f;
        }

        if(Vector3.Distance(transform.position,target.transform.position) > attackRange)
        {
            bossState = BossState.Move;
        }
    }

    public override void DecreaseHP(int attackDamage)
    {
        base.DecreaseHP(attackDamage);

        slider.value = (float)currentHp / maxHp;
        text.text = ((int)((float)currentHp*100 / maxHp)).ToString() + "%";

        if (currentHp <= 0)
        {
            text.text = "0%";
            bossState = BossState.Die;
            //skillState = SkillState.None;
        }
    }

    protected override void Die()
    {
        base.Die();
        sphereCollider.enabled = false;
        IceBallStopZone.SetActive(false);
        GameManager.instance.isFightBoss = false;
        stormCloud.SetActive(false);
        Destroy(gameObject, 3.0f);
    }

    void Forward()
    {
        if (bossState == BossState.Die)
            return;
        Vector3 forward = target.transform.position - transform.position;
        forward.y = 0f;
        transform.forward = forward;
    }

    void AppearHPUI()
    {
        if (GameManager.instance.isFightBoss)
            slider.gameObject.SetActive(true);
        else
            slider.gameObject.SetActive(false);
    }
}
