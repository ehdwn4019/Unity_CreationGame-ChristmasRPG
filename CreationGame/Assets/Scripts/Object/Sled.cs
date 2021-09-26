using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sled : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Santa santa;

    [SerializeField]
    ParticleSystem[] effects;

    Vector3 startPos;
    Rigidbody rigid;

    [SerializeField]
    float speed;

    bool isStart;
    bool isStop;
    bool isPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        startPos = transform.position;
        isStart = false;
        isStop = false;
        isPlaySound = false;
    }

    private void FixedUpdate()
    {
        ResetPos();
        Move();
    }

    void Move()
    {
        if (isStart && !isStop)
        {
            if(isPlaySound)
            {
                SoundManager.instance.PlaySoundEffect("썰매");
                isPlaySound = false;
            }
            
            rigid.MovePosition(transform.position + transform.rotation * Vector3.forward.normalized * speed * Time.fixedDeltaTime);
            for(int i=0; i<effects.Length; i++)
            {
                effects[i].Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && santa.IsClearQuest)
        {
            StartCoroutine("WaitStart");
        }
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2.0f);
        isStart = true;
        isPlaySound = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="BossZone")
        {
            SoundManager.instance.StopAllSoundEffect();
            isStop = true;
            isStart = false;
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].Stop();
            }
        }
    }

    //썰매 원래 자리로 돌아오기
    void ResetPos()
    {
        if (isStart == true && player.IsPlayerFall)
        {
            isStart = false;
            transform.position = startPos;
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].Stop();
            }
        }
    }
}
