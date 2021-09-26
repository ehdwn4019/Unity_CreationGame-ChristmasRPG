using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Player : MonoBehaviour , IDamageable
{
    public Text potionCountText;

    [SerializeField]
    GameObject resetPosition;

    [SerializeField]
    LayerMask enemyLayer;

    [SerializeField]
    Slider hpSlider;

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
    GameObject slotsParent; 

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
    PlayerInventory inven;
    InventorySlot invenSlot;
    InventorySlot[] slots;

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
    public bool IsPlayerFall { get { return isPlayerFall;} set { isPlayerFall = value; } }
    public bool IsTouchPotionBtn { get { return isTouchPotionBtn; } }

    // Start is called before the first frame update
    void Start()
    {
        joyStickMove = FindObjectOfType<JoyStickMove>();
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        comboAttack = GetComponent<PlayerComboAttack>();
        inven = GetComponent<PlayerInventory>();
        cam = Camera.main;
        currentHp = maxHp;
    }

    private void Update()
    {
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
        if (comboAttack.IsAttack || isDie) // || (joyStickMove.isTouch && Input.GetMouseButton(1))
            return;

        float h = 0;
        float v = 0;

        //컴퓨터 모드
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

            //방향에 따라 이동 애니메이션 변경
            if (h < 0)
                animator.SetBool("LeftRun", true);
            else
                animator.SetBool("LeftRun", false);

            if (h > 0)
                animator.SetBool("RightRun", true);
            else
                animator.SetBool("RightRun", false);
        }
        //움직이지 않을때 
        else if(movePos==Vector3.zero)
        {
            isMove = false;
            animator.SetBool("Run", false);
            animator.SetBool("LeftRun", false);
            animator.SetBool("RightRun", false);
        }
    }

    //점프 버튼 터치
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

            //점프
            if (Input.GetKey(KeyCode.Space) && !isJump && isGround)
            {
                SoundManager.instance.PlaySoundEffect("플레이어점프");
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
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
                SoundManager.instance.PlaySoundEffect("플레이어점프");
                animator.SetTrigger("Jump");
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                isJump = true;
                isGround = false;
            }

            isTouchJumpBtn = false;
        }
    }

    //Hp감소
    public void DecreaseHP(int attackDamage)
    {
        currentHp -= attackDamage;
        hpSlider.value = currentHp / maxHp;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    //포션 버튼 터치
    public void TouchPotion()
    {
        isTouchPotionBtn = true;
    }

    //Hp회복
    public void RecoveryHp(int recovery = 0)
    {
        if (isDie)
            return;

        //포션 버튼이나 회복키 클릭
        if(recovery == 0)
        {
            if (isTouchPotionBtn || Input.GetKeyDown(KeyCode.LeftAlt))
            {

                //인벤토리에서도 개수 변경 
                for(int i=0; i<slots.Length; i++)
                {
                    if(slots[i].item !=null)
                    {
                        if(slots[i].item.itemType == Item.ItemType.Potion)
                        {
                            SoundManager.instance.PlaySoundEffect("포션");
                            slots[i].SetPotionCount();
                            currentHp += amountPotion;
                            hpSlider.value = currentHp / maxHp;
                        }
                    }
                }
            }
        }
        //슬롯 클릭해서 회복
        else
        {
            Debug.Log("회복!");
            SoundManager.instance.PlaySoundEffect("포션");
            currentHp += amountPotion;
            hpSlider.value = currentHp / maxHp;
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
            rigidbody.velocity = Vector3.zero;
            StartCoroutine("PlayerFall");
        }

        if(other.gameObject.name == "BossStartZone")
        {
            SoundManager.instance.PlaySoundBgm("보스");
            StartCoroutine("AppearZone");
            resetPosition.transform.position = bossPlayerPos.transform.position;
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

        //필드 아이템 획득
        if(other.gameObject.tag == "Item")
        {
            Item item = other.gameObject.GetComponent<ItemPickUp>().item;
            inven.AcquireItem(item);
            Destroy(other.gameObject);
        }
    }

    IEnumerator AppearZone()
    {
        yield return new WaitForSeconds(1.0f);
        iceBallStopZone.SetActive(true);
    }

    IEnumerator PlayerFall()
    {
        yield return new WaitForSeconds(1.5f);
        isPlayerFall = false;
    }
}
