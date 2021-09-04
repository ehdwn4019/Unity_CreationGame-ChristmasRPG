using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack : MonoBehaviour
{
    Animator animator;
    Player player;
    bool isTouchAttackBtn;
    bool isAttack;

    [SerializeField]
    float attackDelay = 1.0f;

    [SerializeField]
    float attackRange = 0.5f;

    [SerializeField]
    int attackMinDamage = 3;

    [SerializeField]
    int attackMaxDamage = 7;

    [SerializeField]
    BoxCollider attackRangeColl;

    public bool IsAttack { get { return isAttack; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    public void TouchAttack()
    {
        isTouchAttackBtn = true;
    }

    //플레이어 공격
    public void Attack()
    {
        if (player.IsMove || player.IsDie)
            return;

        if (GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if (Input.GetKeyDown(KeyCode.Z)) // 나중에 f키로 바꾸기 
            {
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if (isTouchAttackBtn)
            {
                animator.SetTrigger("Attack");
            }

            isTouchAttackBtn = false;
        }
    }

    public void FindEnemy()
    {
        int attackDamage = Random.Range(attackMinDamage, attackMaxDamage);

        //주변 콜라이더 탐색 
        Collider[] coll = Physics.OverlapSphere(attackRangeColl.transform.position, attackRange*2, LayerMask.GetMask("Enemy"));

        foreach (Collider c in coll)
        {
            Enemy enemy = c.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.DecreaseHP(attackDamage);
            }
        }
    }

    public void AttackTrue()
    {
        isAttack = true;
        attackRangeColl.enabled = true;
    }

    public void CollFalse()
    {
        attackRangeColl.enabled = false;
    }

    public void AttackFalse()
    {
        isAttack = false;
    }
}
