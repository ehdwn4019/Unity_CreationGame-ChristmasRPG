using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //Enemy Type 공통 변수 

    [SerializeField]
    protected float findRange;

    [SerializeField]
    protected float moveRange;

    [SerializeField]
    protected float attackRange;
    
    protected Animator animator;
    public NavMeshAgent nav;
    protected GameObject target;
    protected Rigidbody rigidbody;
    //protected SkinnedMeshRenderer renderer;
    //protected Color startColor;

    //스탯
    protected int currentHp;
    protected int maxHp;
    protected int attackspeed;

    //Enemy enums 
    public enum EnemyState 
    {
        None,
        Idle,
        Move,
        Attack,
        Return,
        Die,
    }

    public EnemyState state;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        //renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        //renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        EnemyPattern();
        Loop();
    }

    protected virtual void Loop()
    {

    }

    //Enemy 패턴 
    void EnemyPattern()
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    #region 상태 함수들 애니메이션 기본 셋팅

    protected virtual void Idle()
    {
        animator.SetBool("Run", false);
    }

    protected virtual void Move()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);
    }

    protected virtual void Attack()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
    }

    protected virtual void Return()
    {
        animator.SetBool("Run", true);
    }

    protected virtual void Die()
    {
        animator.SetBool("Attack", false);
        animator.SetTrigger("Die");
    }

    #endregion

    private void OnDrawGizmos()
    {
        //find 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);

        //move
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRange);

        //attack
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    //hp 감소 함수
    public virtual void DecreaseHP(int attackDamage)
    {
        currentHp -= attackDamage;
    }
}
