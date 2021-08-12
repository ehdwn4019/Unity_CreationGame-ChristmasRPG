using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Enemy
{
    Vector3 monsterStartPos;
    

    [SerializeField]
    float responeTime = 3f;
    [SerializeField]
    float lookSpeed = 10f;
    [SerializeField]
    SphereCollider collider;
    [SerializeField]
    Image questionMark;

    //활성화 했을 때 세팅 
    private void OnEnable()
    {
        hp = 50;
        attackPower = 3;
        collider.enabled = true;
        state = EnemyState.Idle;
    }

    protected override void Init()
    {
        base.Init();
        monsterStartPos = transform.position;
        questionMark.enabled = false;
    }

    protected override void Loop()
    {
        base.Loop();

        //탐지범위 접근시 물음표 이미지 회전
        if (questionMark.enabled == true)
            RotateQuestionMark();

        Debug.Log("HP : " + hp);
    }

    protected override void Idle()
    {
        base.Idle();

        //탐지 범위에 들어올때
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            Debug.Log("Find");

            //부드럽게 쳐다보기
            Vector3 forward = target.transform.position- transform.position;
            Quaternion quaternion = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion , Time.deltaTime * lookSpeed);
            questionMark.enabled = true;

            //이동 범위에 들어올 때
            if (Vector3.Distance(transform.position, target.transform.position) < moveRange)
            {
                state = EnemyState.Move;
            }
        }
        else
        {
            questionMark.enabled = false;
        }
    }


    protected override void Move()
    {
        questionMark.enabled = false;
        base.Move();
        nav.SetDestination(target.transform.position);

        //이동범위보다 멀어질때 
        if (Vector3.Distance(transform.position, monsterStartPos) > moveRange)
        {
            questionMark.enabled = false;
            base.Move();
            state = EnemyState.Return;
        }

        //공격범위에 들어왔을 때 
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            state = EnemyState.Attack;
        }

    }

    protected override void Attack()
    {
        base.Attack();

        int attackDamage = Random.Range(2, 5);

        //GameManager.instance.Player.DecreaseHP(attackDamage);

        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            state = EnemyState.Move;
        }
    }

    protected override void Return()
    {
        base.Return();

        nav.SetDestination(monsterStartPos);

        if ((Vector3.Distance(monsterStartPos,transform.position) <0.1f)|| Vector3.Distance(transform.position,target.transform.position) < attackRange )
        {
            state = EnemyState.Idle;
        }
    }

    public override void DecreaseHP(int attackDamage)
    {
        base.DecreaseHP(attackDamage);
        if (hp <= 0)
        {
            hp = 0;
            state = EnemyState.Die;
        }
    }

    protected override void Die()
    {
        base.Die();
        //잘 안되면 이걸로 사용해보기
        nav.isStopped = true;
        nav.enabled = false;
        collider.enabled = false;

        StartCoroutine("Hide");
        
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(responeTime);

        MonsterSpawn.instance.Disappear(gameObject);
    }

    //물음표 마크 회전 
    void RotateQuestionMark()
    {
        Vector3 euler = new Vector3(0, 90, 0) *2f* Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(euler);
        Vector3 eulerRotation = rotation.eulerAngles;
        questionMark.transform.Rotate(eulerRotation);
    }
}
