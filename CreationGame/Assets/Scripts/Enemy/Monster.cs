using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
//using System;

public class Monster : Enemy
{
    [SerializeField]
    float responeTime = 3f;

    [SerializeField]
    float lookSpeed = 10f;

    [SerializeField]
    SphereCollider collider;

    [SerializeField]
    Image questionMark;

    [SerializeField]
    Slider slider;
    
    BoardQuest boardQuest;
    //GameObject inGameCanvas;

    
    Vector3 startPos;
    //public event Action<int> monsterQuest;
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
        collider.enabled = true;
    }

    protected override void Init()
    {
        base.Init();
        boardQuest = FindObjectOfType<BoardQuest>();
        //inGameCanvas = GameObject.Find("InGameCanvas");
        startPos = transform.position;
        questionMark.enabled = false;
    }

    protected override void Loop()
    {
        base.Loop();

        //탐지범위 접근시 물음표 이미지 회전
        if (questionMark.enabled == true)
            RotateQuestionMark();

        //MonsterQuest();

        //if (monsterQuest != null)
        //{
        //    Debug.Log("AAAAAAAAAAAA");
        //    monsterQuest.Invoke(1);
        //}
        //else
        //    Debug.Log("대체왜ㅑ애ㅔ에에에에");

        //Debug.Log("HP : " + currentHp);
        //Debug.Log("Slider Value : " + slider.value);
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
            //부드럽게 쳐다보기
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

        int attackDamage = Random.Range(2, 5); //UnityEngine.Random.Range(2, 5);

        attackDelay -= Time.deltaTime;

        if(attackDelay<=0f)
        {
            //attackDelay += Time.deltaTime;
            if (target != null)
                target.GetComponent<IDamageable>().DecreaseHP(attackDamage);

            //if(attackDelay>=0.9f)
                attackDelay = 0.89f;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
        {
            monsterState = MosterState.Move;
        }
    }

    protected override void Return()
    {
        base.Return();

        nav.SetDestination(startPos);

        if ((Vector3.Distance(startPos, transform.position) <0.55f)|| Vector3.Distance(transform.position,target.transform.position) < attackRange )
        {
            monsterState = MosterState.Idle;
        }
    }

    public override void DecreaseHP(int attackDamage)
    {
        Debug.Log("호출");
        base.DecreaseHP(attackDamage);
        slider.value = (float)currentHp / maxHp;
        //StartCoroutine("ChangeRendererColor");

        if (currentHp <= 0)
        {
            currentHp = 0;
            isHpZero = true;
            //boardQuest.SetQuestText(1);
            //StartCoroutine("HpZeroDelay");
            //boardQuest.currentKillMonster++;
            //boardQuest.SetQuestText(1);

            if(boardQuest.IsTouchRewardBtn() == false)
                StartCoroutine("MonsterQuest");
            //Invoke("MonsterQuest", 3.0f);

            monsterState = MosterState.Die;
        }
    }

    IEnumerator MonsterQuest()
    {
        yield return new WaitForSeconds(1.0f);

        if (boardQuest.monsterQuest != null && isHpZero)
        {
            boardQuest.monsterQuest.Invoke(1);
            isHpZero = false;
        }
    }

    //IEnumerator ChangeRendererColor()
    //{
    //    renderer.material.color = Color.red;
    //
    //    yield return new WaitForSeconds(0.25f);
    //
    //    renderer.material.color = startColor;
    //}

    protected override void Die()
    {
        base.Die();
        //잘 안되면 이걸로 사용해보기
        //nav.isStopped = true;
        
        nav.enabled = false;
        collider.enabled = false;
        slider.gameObject.SetActive(false);
        StartCoroutine("Hide");
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(responeTime);

        MonsterSpawn.instance.Disappear(gameObject);

        //isHpZero = false;
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

    //void MonsterQuest()
    //{
    //    if (boardQuest.monsterQuest != null)
    //        boardQuest.monsterQuest.Invoke(1);
    //}
}
