using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
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

    Vector3 startPos;

    [SerializeField]
    float lookSpeed = 10f;

    float attackDelay = 1.0f;
    bool isDie;

    public bool IsDie { get { return isDie; } }
    
    protected override void Init()
    {
        base.Init();
        bossState = BossState.Idle;
        startPos = transform.position;
        isDie = false;
        maxHp = 100;
        currentHp = maxHp;
    }

    protected override void Loop()
    {
        base.Loop();
        Forward();
        AppearHPUI();
    }

    protected override void Idle()
    {
        //플레이어 죽었을때 idle 상태로 돌리기 
        base.Idle();
        nav.ResetPath();

        if (!GameManager.instance.isFightBoss)
            return;

        if (Vector3.Distance(transform.position, target.transform.position) < moveRange)
        {
            bossState = BossState.Move;
        }
        else
        {
            bossState = BossState.FirstSkill;
            throwSkill.Throw();
        }
    }

    protected override void Move()
    {
        base.Move();
        nav.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            bossState = BossState.Attack;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > moveRange)
        {
            bossState = BossState.Idle;
        }
    }

    protected override void Attack()
    {
        base.Attack();

        int attackDamage = Random.Range(8, 10);

        attackDelay -= Time.deltaTime;

        if(attackDelay<=0f)
        {
            if (target != null)
                target.GetComponent<IDamageable>().DecreaseHP(attackDamage);

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
        }
    }

    protected override void Die()
    {
        base.Die();
        sphereCollider.enabled = false;
        IceBallStopZone.SetActive(false);
        GameManager.instance.isFightBoss = false;
        stormCloud.SetActive(false);
        isDie = true;
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
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }  
    }
}
