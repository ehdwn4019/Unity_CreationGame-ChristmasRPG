using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    float coolTime = 15.0f;

    [SerializeField]
    float buffTime = 10.0f;

    [SerializeField]
    float currentCoolTime = 0;

    [SerializeField]
    ParticleSystem fireEffectSword;

    [SerializeField]
    ParticleSystem fireEffectBody;

    [SerializeField]
    Slider slider;

    [SerializeField]
    GameObject fireEffectBodyObj;

    Player player;

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

    public void TouchSkill()
    {
        isTouchFireBuff = true;
    }

    public void FireBuff()
    {
        if (player.IsDie)
            return;

        if(GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl) && !isFireBuff && !gageDown)
            {
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
                isFireBuff = true;
                gageDown = true;
                slider.transform.SetAsLastSibling();
                StartCoroutine("FireBuffON");
            }

            isTouchFireBuff = false;
        }
    }

    IEnumerator FireBuffON()
    {
        fireEffectSword.Play();
        fireEffectBodyObj.SetActive(true);
        fireEffectBody.Play();
        Debug.Log("쿨타임");
        yield return new WaitForSeconds(buffTime);
        isFireBuff = false;
        fireEffectSword.Stop();
        fireEffectBodyObj.SetActive(false);
        fireEffectBody.Stop();
    }

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
