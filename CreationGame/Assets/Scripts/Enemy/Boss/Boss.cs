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

    Vector3 startPos;
    float attackDelay = 1.0f;
    //float skillDelay = 2.0f;

    public enum SkillState
    {
        None,
        Throw,
        JumpingDown,
    }

    public SkillState skillState = SkillState.None;

    protected override void Init()
    {
        base.Init();
        state = EnemyState.Idle;
        startPos = transform.position;
        maxHp = 100;
        currentHp = maxHp;
    }

    protected override void Loop()
    {
        base.Loop();
        Forward();
        AppearHPUI();
        Debug.Log("state : "+state);
        Debug.Log("skillstate : "+skillState);
    }

    protected override void Idle()
    {
        //플레이어 죽었을때 idle 상태로 돌리기 
        base.Idle();

        if (!GameManager.instance.isFightBoss)
            return;


        nav.ResetPath();

        //skillDelay -= Time.deltaTime;
        //Debug.Log(skillDelay);

        int random = Random.Range(0, 2);

        //Debug.Log(random);

        if (Vector3.Distance(transform.position, target.transform.position) < moveRange)
        {
            //skillDelay = 4.0f;
            state = EnemyState.Move;
            skillState = SkillState.None;

        }
        else
        {
            //skillDelay = 4.0f;
           

            switch (random)
            {
                //눈던지기
                case 0:
                    state = EnemyState.None;
                    skillState = SkillState.Throw;
                    break;
                //점프내려찍기
                case 1:
                    state = EnemyState.None;
                    skillState = SkillState.JumpingDown;
                    break;
                    ////눈뿌리기
                    //case 2:
                    //    state = EnemyState.None;
                    //    skillState = SkillState.Spray;
                    //    break;
            }

        }

        //if (skillDelay <=0)
        //{
        //
        //}

        

        //if(Vector3.Distance(transform.position,target.transform.position)<findRange)
        //{
        //    //this.transform.LookAt(target.transform);
        //
        //    Vector3 forward = target.transform.position - transform.position;
        //    Quaternion quaternion = Quaternion.LookRotation(forward);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * lookSpeed);
        //
        //    if (Vector3.Distance(transform.position, target.transform.position)< moveRange)
        //    {
        //        state = EnemyState.Move;
        //    }
        //}
    }

    protected override void Move()
    {
        base.Move();
        nav.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) > moveRange)
        {
            state = EnemyState.Idle;
            skillState = SkillState.None;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            state = EnemyState.Attack;
            skillState = SkillState.None;
        }
        //else if(Vector3.Distance(transform.position,target.transform.position)>attackRange && Vector3.Distance(transform.position, target.transform.position)<findRange)
        //{
        //    state = EnemyState.Move;
        //    skillState = SkillState.None;
        //}
        

            

        //int random = Random.Range(0, 2);

        //if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        //{
        //
        //    state = EnemyState.Idle;
        //    skillState = SkillState.None;
        //    //switch (random)
        //    //{
        //    //    //추적
        //    //    case 0:
        //    //        skillState = SkillState.None;
        //    //        state = EnemyState.Move;
        //    //        break;
        //    //    //눈던지기
        //    //    case 1:
        //    //        state = EnemyState.None;
        //    //        skillState = SkillState.Throw;
        //    //        //Throw();
        //    //        break;
        //    //        ////눈뿌리기
        //    //        //case 2:
        //    //        //    state = EnemyState.None;
        //    //        //    skillState = SkillState.Spray;
        //    //        //    break;
        //    //
        //    //        //점프내려찍기
        //    //        //case 2:
        //    //        //    state = EnemyState.None;
        //    //        //    skillState = SkillState.JumpingDown;
        //    //        //    //JumpingDown();
        //    //        //    break;
        //    //}
        //}
        //else
        //{
        //    state = EnemyState.Attack;
        //    skillState = SkillState.None;
        //}

        //else
        //{
        //    state = EnemyState.Idle;
        //}

        //if(Vector3.Distance(transform.position,target.transform.position) > moveRange)
        //{
        //    state = EnemyState.Return;
        //}
    }

    protected override void Attack()
    {
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
            state = EnemyState.Move;
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

        slider.value = (float)currentHp / maxHp;
        text.text = ((int)((float)currentHp*100 / maxHp)).ToString() + "%";

        if (currentHp <= 0)
        {
            text.text = "0%";
            state = EnemyState.Die;
            skillState = SkillState.None;
        }
    }

    protected override void Die()
    {
        base.Die();
        //StartCoroutine("DieCoroutine");
        sphereCollider.enabled = false;
        IceBallStopZone.SetActive(false);
        GameManager.instance.isFightBoss = false;
        stormCloud.SetActive(false);
        Destroy(gameObject, 3.0f);
    }

    void Forward()
    {
        if (state == EnemyState.Die)
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

    //IEnumerator DieCoroutine()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //}

    ////눈던지기
    //void Throw()
    //{
    //    transform.forward = target.transform.position - transform.position;
    //    animator.SetTrigger("Throw");
    //}

    ////점프내려찍기
    //void JumpingDown()
    //{
    //    animator.SetTrigger("JumpingDown");
    //}

}
