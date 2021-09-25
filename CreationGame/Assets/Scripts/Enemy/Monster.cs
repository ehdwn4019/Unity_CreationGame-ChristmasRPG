using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : Enemy
{
    [SerializeField]
    SphereCollider collider;

    [SerializeField]
    Image questionMark;

    [SerializeField]
    Slider slider;

    BoardQuest boardQuest;
    Vector3 startPos;

    [SerializeField]
    float responeTime = 3f;

    [SerializeField]
    float lookSpeed = 10f;
    
    float attackDelay = 0.89f;
    bool isHpZero;

    //활성화 했을 때 세팅 
    private void OnEnable()
    {
        monsterState = MosterState.Idle;
        maxHp = 50;
        currentHp = 50;
        attackspeed = 3;
        slider.value = 1;
        isDie = false;
        collider.enabled = true;
    }

    protected override void Init()
    {
        base.Init();
        boardQuest = FindObjectOfType<BoardQuest>();
        startPos = transform.position;
        questionMark.enabled = false;
    }

    protected override void Loop()
    {
        base.Loop();

        //탐지범위 접근시 물음표 이미지 회전
        if (questionMark.enabled == true)
            RotateQuestionMark();
    }

    protected override void Idle()
    {
        base.Idle();

        //Hp 회복
        if (currentHp != maxHp)
        {
            currentHp++;
            slider.value += Time.deltaTime;
        }

        //탐지 범위에 들어올때
        if (Vector3.Distance(transform.position, target.transform.position) < findRange)
        {
            //회전하면서 바라보기 
            Vector3 forward = target.transform.position- transform.position;
            Quaternion quaternion = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion , Time.deltaTime * lookSpeed);
            questionMark.enabled = true;

            //이동 범위에 들어올 때
            if (Vector3.Distance(transform.position, target.transform.position) < moveRange)
            {
                monsterState = MosterState.Move;
            }
        }
        else
        {
            //회전하면서 바라보기
            Quaternion quaternion = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * lookSpeed);
            questionMark.enabled = false;
        }
    }


    protected override void Move()
    {
        questionMark.enabled = false;
        base.Move();
        nav.SetDestination(target.transform.position);
        
        //이동범위보다 멀어질때 
        if (Vector3.Distance(transform.position, startPos) > moveRange)
        {
            questionMark.enabled = false;
            base.Move();
            monsterState = MosterState.Return;
        }

        //공격범위에 들어왔을 때 
        if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
        {
            monsterState = MosterState.Attack;
        }
    }

    protected override void Attack()
    {
        transform.forward = target.transform.position - transform.position;

        base.Attack();

        int attackDamage = Random.Range(2, 5); 

        attackDelay -= Time.deltaTime;

        if(attackDelay<=0f)
        {
            Player player = target.GetComponent<Player>();

            if (target != null && !player.IsDie)
            {
                SoundManager.instance.PlaySoundEffect("몬스터공격");
                target.GetComponent<IDamageable>().DecreaseHP(attackDamage);
            }
                

            attackDelay = 0.89f;
        }

        //공격범위보다 멀어질때
        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            monsterState = MosterState.Move;
        }
    }

    protected override void Return()
    {
        base.Return();

        nav.SetDestination(startPos);

        //처음 위치에서 많이 벗어났을때
        if ((Vector3.Distance(startPos, transform.position) <0.55f)|| Vector3.Distance(transform.position,target.transform.position) < attackRange )
        {
            monsterState = MosterState.Idle;
        }
    }

    public override void DecreaseHP(int attackDamage)
    {
        base.DecreaseHP(attackDamage);
        slider.value = (float)currentHp / maxHp;

        if (currentHp <= 0)
        {
            currentHp = 0;
            isHpZero = true;
            isDie = true;

            if(boardQuest.IsTouchRewardBtn() == false)
            {
                StartCoroutine("MonsterQuest");
            }

            monsterState = MosterState.Die;
        }
    }

    //몬스터 잡기 퀘스트 마리수 증가
    IEnumerator MonsterQuest()
    {
        yield return new WaitForSeconds(1.0f);

        if (boardQuest.monsterQuest != null && isHpZero)
        {
            boardQuest.monsterQuest.Invoke(1);
            isHpZero = false;
        }
    }

    protected override void Die()
    {
        base.Die();
        nav.enabled = false;
        collider.enabled = false;
        slider.gameObject.SetActive(false);
        StartCoroutine("Hide");
    }

    //몬스터 리스폰 코루틴 
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(responeTime);

        MonsterSpawn.instance.Disappear(gameObject);

        Invoke("ReturnPos", 3.0f);
    }

    void ReturnPos()
    {
        MonsterSpawn.instance.Appear(startPos);
        nav.enabled = true;
        slider.gameObject.SetActive(true);
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
