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
    GameObject basePos;

    [SerializeField]
    GameObject leftPlayerPos;

    [SerializeField]
    GameObject rightPlayerPos;

    [SerializeField]
    GameObject bossPlayerPos;

    [SerializeField]
    GameObject bossPos;

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

    [SerializeField]
    int amountPotion = 20;

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
    bool isTouchPotionBtn;
    bool isPlayerFall;

    public bool IsDie { get { return isDie; } }
    public bool IsMove { get { return isMove; } }
    public bool IsJump { get { return isJump; } }
    public bool IsPlayerFall { get { return isPlayerFall; } }

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

    private void Update()
    {
        //GameManager.instance.playerDie += () => { Debug.Log("GG"); };
        RecoveryHp();
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
                
            }

            if (isTouchJumpBtn && !isJump && isGround)
            {
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }

            isTouchJumpBtn = false;
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

    public void TouchPotion()
    {
        isTouchPotionBtn = true;
    }

    public void RecoveryHp()
    {
        if (isDie)
            return;

        if(isTouchPotionBtn || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //Debug.Log("회복!");
            currentHp += amountPotion;
            hpSlider.value = currentHp/maxHp;
        }
            
        if(currentHp>=maxHp)
        {
            currentHp = maxHp;
            hpSlider.value = currentHp/maxHp;
        }

        isTouchPotionBtn = false;
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
            isPlayerFall = true;
            StartCoroutine("PlayerFall");
        }

        if(other.gameObject.name == "BossStartZone")
        {
            resetPosition.transform.position = bossPlayerPos.transform.position;
            StartCoroutine("AppearZone");
            GameManager.instance.isFightBoss = true;
        }

        if(other.gameObject.name == "BossZone")
        {
            resetPosition.transform.position = bossPos.transform.position;
        }

        if (other.gameObject.name == "LeftStartZone")
        {
            resetPosition.transform.position = leftPlayerPos.transform.position;
        }

        if (other.gameObject.name == "RightStartZone")
        {
            resetPosition.transform.position = rightPlayerPos.transform.position;
        }

        if (other.gameObject.tag == "Portal")
        {
            transform.position = basePos.transform.position;
        }
    }

    IEnumerator AppearZone()
    {
        yield return new WaitForSeconds(1.0f);
        iceBallStopZone.SetActive(true);
    }

    IEnumerator PlayerFall()
    {
        yield return new WaitForSeconds(0.5f);
        isPlayerFall = false;
    }
}
