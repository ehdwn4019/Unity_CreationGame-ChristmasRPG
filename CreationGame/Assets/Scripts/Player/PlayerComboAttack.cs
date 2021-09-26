using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttack : MonoBehaviour
{
    [SerializeField]
    BoxCollider attackRangeColl;

    [SerializeField]
    ParticleSystem damageText;

    Animator animator;
    Player player;
    PlayerFireBuff fireBuff;
    Enemy enemy;

    [SerializeField]
    float attackDelay = 1.0f;

    [SerializeField]
    float attackRange = 0.5f;

    [SerializeField]
    int attackMinDamage = 3;

    [SerializeField]
    int attackMaxDamage = 7;

    [SerializeField]
    int fireDamage = 3;

    bool isTouchAttackBtn;
    bool isAttack;

    public bool IsAttack { get { return isAttack; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        fireBuff = GetComponent<PlayerFireBuff>();
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        damageText.Stop();
    }

    private void Update()
    {
        Attack();
    }

    //어택 버튼 터치
    public void TouchAttack()
    {
        isTouchAttackBtn = true;
    }

    //플레이어 공격
    public void Attack()
    {
        if (player.IsMove || player.IsDie)
            return;

        //컴퓨터 모드 
        if (GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if (Input.GetKeyDown(KeyCode.Z)) 
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

    //Enemy 찾기 
    public void FindEnemy()
    {
        int attackDamage = Random.Range(attackMinDamage, attackMaxDamage);

        //주변 콜라이더 탐색 
        Collider[] coll = Physics.OverlapSphere(attackRangeColl.transform.position, attackRange*1.5f, LayerMask.GetMask("Enemy"));

        foreach (Collider c in coll)
        {
            //주변 Enemy damage interface 가져오기 
            IDamageable damage = c.GetComponent<IDamageable>();
            Enemy enemy = c.GetComponent<Enemy>();

            if(damage != null && !enemy.IsDie)
            {
                //데미지 증가
                if (fireBuff.IsFireBuff)
                {
                    attackDamage += fireDamage;
                }

                Random.InitState(100);

                //충돌 이펙트 생성 
                damageText.transform.position = enemy.transform.position+new Vector3(0f,enemy.GetComponent<SphereCollider>().radius*2,enemy.GetComponent<SphereCollider>().radius*-4.0f);
                damageText.Play();

                SoundManager.instance.PlaySoundEffect("플레이어공격");
                damage.DecreaseHP(attackDamage);
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
