using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    [SerializeField]
    GameObject resetPosition;

    //[SerializeField]
    //SphereCollider attackRangeColl;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Slider hpSlider;

    [SerializeField]
    Text potion;

    Rigidbody rigidbody;
    Animator animator;
    Camera cam;
    JoyStickMove joyStickMove;
    Enemy target;
    GameObject attackColl;

    [SerializeField]
    float moveSpeed = 5.0f;

    [SerializeField]
    float jumpspeed = 5.0f;

    [SerializeField]
    float jumpRange = 0.3f;

    [SerializeField]
    float attackDelay = 1.0f;

    [SerializeField]
    float attackRange = 0.5f;

    [SerializeField]
    float maxHp = 100f;

    [SerializeField]
    float currentHp;

    //[SerializeField]
    //int attackDamage;

    bool isMove;
    bool isAttack;
    bool isJump;
    bool isGround;
    bool isDie;

    bool isTouchAttackBtn;
    bool isTouchJumpBtn;

    public bool IsDie { get { return isDie; } }

    
    
 

    // Start is called before the first frame update
    void Start()
    {
        //attackRangeColl.enabled = false;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        attackColl = GameObject.Find("PlayerAttackColl");
        cam = Camera.main;
        joyStickMove = FindObjectOfType<JoyStickMove>();
        currentHp = maxHp;
        attackColl.SetActive(false);
        //AnimationAddEvent(1, "Attack", 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    //플레이어 이동
    void Move()
    {
        if (isAttack || isDie || (joyStickMove.isTouch && Input.GetMouseButton(1)))
            return;

        float h = 0;
        float v = 0;

        if(GameManager.instance.ct == GameManager.ControllType.Phone)
        {
            if(joyStickMove.isTouch)
            {
                h = joyStickMove.Value.x;
                v = joyStickMove.Value.y;
            }
        }
        else
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }
        
        Vector3 movePos = new Vector3(h, 0, v);
        rigidbody.MovePosition(transform.position+transform.rotation * movePos.normalized * moveSpeed * Time.fixedDeltaTime);

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

    public void TouchJump()
    {
        isTouchJumpBtn = true;
    }

    //플레이어 점프
    public void Jump()
    {
        if (isAttack||isDie)
            return;

        if(GameManager.instance.ct==GameManager.ControllType.Computer)
        {
            if (Input.GetKey(KeyCode.Space) && !isJump && isGround)
            {
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }

            Debug.DrawRay(transform.position, Vector3.down * jumpRange, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange))
            {
                isJump = false;
            }
        }
        else
        {
            if (isTouchJumpBtn && !isJump && isGround)
            {
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }

            Debug.DrawRay(transform.position, Vector3.down * jumpRange, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange))
            {
                isJump = false;
                isTouchJumpBtn = false;
            }
        }
    }

    public void TouchAttack()
    {
        isTouchAttackBtn = true;
    }

    //플레이어 공격
    public void Attack()
    {
        if (isMove||isDie)
            return;

        if(GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isAttack = true;
                animator.SetTrigger("Attack");
                attackColl.SetActive(true);
                //StartCoroutine("ComboAttack");
            }
        }
        else
        {
            if(isTouchAttackBtn)
            {
                isAttack = true;
                //StartCoroutine("ComboAttack");
            }

            isTouchAttackBtn = false;
            
        }

        isAttack = false;
    }

    //3단 기본 공격
    //IEnumerator ComboAttack()
    //{
    //    animator.SetTrigger("Attack");
    //
    //    //주변 콜라이더 탐색 
    //    Collider[] coll = Physics.OverlapSphere(attackRangeColl.transform.position,attackRange,enemyLayer);
    //    //attackDamage = Random.Range(3, 7);
    //    attackDamage = 1;
    //
    //    foreach (Collider c in coll)
    //    {
    //        attackRangeColl.enabled = true;
    //        Enemy enemy = c.GetComponent<Enemy>();
    //        
    //        if (enemy != null)
    //        {
    //            Debug.Log("공격!!");
    //            enemy.DecreaseHP(attackDamage);
    //        }
    //            
    //        yield return new WaitForSeconds(0.01f);
    //        attackRangeColl.enabled = false;
    //    }
    //
    //    yield return new WaitForSeconds(attackDelay);
    //    isAttack = false;
    //}

    public void DecreaseHP(int attackDamage)
    {
        currentHp -= attackDamage;
        hpSlider.value = currentHp / maxHp;
        if(currentHp<=0)
        {
            Die();
        }
    }

    public void RecoveryHp()
    {
        //potion.text -= 
    }

    //플레이어 사망
    void Die()
    {
        isDie = true;
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
        if (other.gameObject.name == "BasicMapZone")
        {
            transform.position = resetPosition.transform.position;
            DecreaseHP(20);
        }
    }
}
