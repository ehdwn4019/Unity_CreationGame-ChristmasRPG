using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    public GameObject resetPosition;
    public SphereCollider attackRangeColl;
    public LayerMask enemyLayer;
    public Slider hpSlider;

    Rigidbody rigidbody;
    Animator animator;
    Camera cam;
    JoyStick joyStick;
    Enemy target;

    public float moveSpeed = 5.0f;
    public float jumpPower = 5.0f;
    public float jumpRange = 0.3f;
    public float attackDelay = 4.0f;
    
    float attackRange = 0.5f;

    bool isMove;
    bool isAttack;
    bool isJump;
    bool isGround;
    bool isDie;

    public bool IsDie { get { return isDie; } }

    public float currentHp;
    public float maxHp = 100f;
    int attackDamage ;

    // Start is called before the first frame update
    void Start()
    {
        attackRangeColl.enabled = false;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main;
        joyStick = FindObjectOfType<JoyStick>();
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        TryJump();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    //플레이어 이동
    void Move()
    {
        if (isAttack)
            return;

        float h = 0;
        float v = 0;

        if(joyStick.isTouch)
        {
            h = joyStick.Value.x;
            v = joyStick.Value.y;
        }
        else
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        
        Vector3 movePos = new Vector3(h, 0, v);
        transform.Translate(movePos.normalized * moveSpeed * Time.deltaTime);

        if (movePos!=Vector3.zero)
        {
            isMove = true;

            animator.SetBool("Run", true);

            if (h < 0)
                animator.SetBool("LeftRun", true);
            else
                animator.SetBool("LeftRun", false);

            if (h > 0)
                animator.SetBool("RightRun", true);
            else
                animator.SetBool("RightRun", false);
                
        }
        else if(movePos==Vector3.zero)
        {
            isMove = false;
            animator.SetBool("Run", false);
            animator.SetBool("LeftRun", false);
            animator.SetBool("RightRun", false);
        }
    }

    void TryJump()
    {
        if (isAttack)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
            Jump();
        }
    }

    //플레이어 점프
    void Jump()
    {
        //if (isAttack)
        //    return;
        
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, Vector3.down * jumpRange, Color.red);
        
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange))
        //{
        //    isJump = false;
        //}
        //
        //if (Input.GetKeyDown(KeyCode.Space) && !isJump && isGround)
        //{
        if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange) && isJump)
        {
            animator.SetTrigger("Jump");
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        //    isJump = true;
        //    isGround = false;
        //}

        isJump = false;
    }

    //플레이어 공격
    void Attack()
    {
        if (isMove)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            attackRangeColl.enabled = true;
            StartCoroutine("ComboAttack");
        }
    }

    //3단 기본 공격
    IEnumerator ComboAttack()
    {
        animator.SetTrigger("Attack");

        //주변 콜라이더 탐색 
        Collider[] coll = Physics.OverlapSphere(attackRangeColl.transform.position, attackRange, enemyLayer);
        attackDamage = Random.Range(3, 7);
        
        foreach(Collider c in coll)
        {
            c.GetComponent<Enemy>().DecreaseHP(attackDamage);
        }

        //for(int i=0; i<coll.Length; i++)
        //{
        //    coll[i].GetComponent<Enemy>().DecreaseHP(attackDamage);
        //    Debug.Log("무야호");
        //}

        //GameObject go = GameObject.FindGameObjectWithTag("Enemy");
        //RaycastHit hit;
        ////RaycastHit[] rayCastHit = Physics.SphereCastAll(this.transform.position, attackRange,this.transform.forward,attackRange*1.5f,enemyLayer);
        //bool isInRange = Physics.SphereCast(this.transform.position, attackRange, this.transform.forward, out hit, attackRange * 1.5f, enemyLayer);
        //
        //if(isInRange)
        //{
        //    go.GetComponent<Enemy>().DecreaseHP(attackDamage);
        //    Debug.Log("무야호");
        //        
        //}
        
            
        


        //for(int i= 0; i<rayCastHit.Length; i++)
        //{
        //    target.attackPower--;
        //    Debug.Log("target.attackPower : " + target.attackPower);
        //    Debug.Log("무야호");
        //
        //}

        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
        attackRangeColl.enabled = false;
    }


    //플레이어 피격
    void Damaged()
    {
        animator.SetTrigger("Damaged");
    }

    public void DecreaseHP(int attackDamage)
    {
        currentHp -= attackDamage;
        hpSlider.value = currentHp / maxHp;
        if(currentHp<=0)
        {
            Die();
        }
    }

    //플레이어 사망
    void Die()
    {
        animator.SetTrigger("Die");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeadZone")
        {
            transform.position = resetPosition.transform.position;
            DecreaseHP(20);
        }
    }
}
