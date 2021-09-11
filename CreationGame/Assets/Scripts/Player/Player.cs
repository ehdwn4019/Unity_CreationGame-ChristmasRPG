using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player : MonoBehaviour 
{
    [SerializeField]
    GameObject resetPosition;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Slider hpSlider;

    [SerializeField]
    Text potion;

    [SerializeField]
    GameObject iceBallStopZone;

    [SerializeField]
    float moveSpeed = 5.0f;

    [SerializeField]
    float jumpSpeed = 5.0f;

    [SerializeField]
    float jumpRange = 0.3f;

    [SerializeField]
    float maxHp = 100f;

    [SerializeField]
    float currentHp;

    Rigidbody rigidbody;
    Animator animator;
    Camera cam;
    JoyStickMove joyStickMove;
    PlayerComboAttack comboAttack;
    Enemy target;
    GameObject attackColl;

    bool isMove;
    bool isJump;
    bool isGround;
    bool isDie;
    bool isTouchJumpBtn;

    

    public bool IsDie { get { return isDie; } }
    public bool IsMove { get { return isMove; } }
    public bool IsJump { get { return isJump; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //attackColl = GameObject.Find("PlayerAttackColl");
        cam = Camera.main;
        joyStickMove = FindObjectOfType<JoyStickMove>();
        comboAttack = GetComponent<PlayerComboAttack>();
        currentHp = maxHp;
        //attackColl.SetActive(false);
        //AnimationAddEvent(1, "Attack", 0.6f);
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    //플레이어 이동
    void Move()
    {
        if (comboAttack.IsAttack || isDie || (joyStickMove.isTouch && Input.GetMouseButton(1)))
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
        if (comboAttack.IsAttack||isDie)
            return;

        if(GameManager.instance.ct==GameManager.ControllType.Computer)
        {
            Debug.DrawRay(transform.position, Vector3.down * jumpRange, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange))
            {
                isJump = false;
            }

            if (Input.GetKey(KeyCode.Space) && !isJump && isGround)
            {
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                //rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                rigidbody.AddForce(Vector3.up*jumpSpeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * jumpRange, Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, jumpRange))
            {
                isJump = false;
                isTouchJumpBtn = false;
            }

            if (isTouchJumpBtn && !isJump && isGround)
            {
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }
        }
    }

    public void DecreaseHP(int attackDamage)
    {
        currentHp -= attackDamage;
        hpSlider.value = currentHp / maxHp;
        if (currentHp <= 0)
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
        if (other.gameObject.name == "ResponeZone")
        {
            transform.position = resetPosition.transform.position;
            DecreaseHP(20);
        }

        if(other.gameObject.name == "BossZone")
        {
            StartCoroutine("AppearZone");
            GameManager.instance.isFightBoss = true;
        }
    }

    IEnumerator AppearZone()
    {
        yield return new WaitForSeconds(4.0f);
        iceBallStopZone.SetActive(true);
    }
}
