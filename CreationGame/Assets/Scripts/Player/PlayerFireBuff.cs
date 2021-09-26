using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFireBuff : MonoBehaviour
{
    [SerializeField]
    ParticleSystem fireEffectSword;

    [SerializeField]
    ParticleSystem fireEffectBody;

    [SerializeField]
    Slider slider;

    [SerializeField]
    GameObject fireEffectBodyObj;

    Player player;

    [SerializeField]
    float coolTime = 15.0f;

    [SerializeField]
    float buffTime = 10.0f;

    [SerializeField]
    float currentCoolTime = 0;

    bool gageDown;
    bool isFireBuff;
    bool isTouchFireBuff;
    public bool IsFireBuff { get { return isFireBuff; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        fireEffectSword.Stop();
        fireEffectBody.Stop();
        fireEffectBodyObj.SetActive(false);
        currentCoolTime = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        FireBuff();
        BuffCoolTime();
    }

    //스킬 버튼 터치
    public void TouchSkill()
    {
        isTouchFireBuff = true;
    }

    //화염 버프
    public void FireBuff()
    {
        if (player.IsDie)
            return;

        //컴퓨터 모드
        if(GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl) && !isFireBuff && !gageDown)
            {
                SoundManager.instance.PlaySoundEffect("플레이어스킬");
                isFireBuff = true;
                gageDown = true;
                slider.transform.SetAsLastSibling();
                StartCoroutine("FireBuffON");
            }
        }
        else
        {
            if(!isFireBuff && !gageDown && isTouchFireBuff)
            {
                SoundManager.instance.PlaySoundEffect("플레이어스킬");
                isFireBuff = true;
                gageDown = true;
                slider.transform.SetAsLastSibling();
                StartCoroutine("FireBuffON");
            }

            isTouchFireBuff = false;
        }
    }

    //버프 적용 
    IEnumerator FireBuffON()
    {
        fireEffectSword.Play();
        fireEffectBodyObj.SetActive(true);
        fireEffectBody.Play();
        yield return new WaitForSeconds(buffTime);
        isFireBuff = false;
        fireEffectSword.Stop();
        fireEffectBodyObj.SetActive(false);
        fireEffectBody.Stop();
    }

    //버프 사용후 쿨타임 
    void BuffCoolTime()
    {
        if(gageDown)
        {
            currentCoolTime -= Time.deltaTime;
            slider.value = currentCoolTime / coolTime;
            if (currentCoolTime <= 0)
            {
                currentCoolTime = coolTime;
                slider.transform.SetAsFirstSibling();
                
                gageDown = false;
            }
                
        }
        
    }
}
