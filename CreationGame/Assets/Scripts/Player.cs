using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    [SerializeField]
    GameObject resetPosition;
    [SerializeField]
    SphereCollider attackRangeColl;
    [SerializeField]
    LayerMask enemyLayer;
    [SerializeField]
    Slider hpSlider;

    Rigidbody rigidbody;
    Animator animator;
    Camera cam;
    JoyStick joyStick;
    Enemy target;

    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float jumpspeed = 5.0f;
    [SerializeField]
    float jumpRange = 0.3f;
    [SerializeField]
    float attackDelay = 4.0f;
    [SerializeField]
    float attackRange = 0.5f;

    bool isMove;
    bool isAttack;
    bool isJump;
    bool isGround;
    bool isDie;

    public bool IsDie { get { return isDie; } }

    [SerializeField]
    float currentHp;
    [SerializeField]
    float maxHp = 100f;
    [SerializeField]
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

    //플레이어 점프
    void Jump()
    {
        if (isAttack)
            return;

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
            Enemy enemy = c.GetComponent<Enemy>();
            if(enemy !=null)
                enemy.DecreaseHP(attackDamage);
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
        //    target.attackspeed--;
        //    Debug.Log("target.attackspeed : " + target.attackspeed);
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

        //if (collision.gameObject.tag == "Enemy")
        //{
        //    rigidbody.isKinematic = true;
        //    //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        rigidbody.isKinematic = false;
    //        //collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BasicMapZone")
        {
            transform.position = resetPosition.transform.position;
            DecreaseHP(20);
        }
    }
}
