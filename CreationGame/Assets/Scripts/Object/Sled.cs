using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sled : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    float speed;

    bool isStart;
    bool isStop;

    Vector3 startPos;

    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        startPos = transform.position;
        isStart = false;
        isStop = false;
    }

    private void FixedUpdate()
    {
        ResetPos();
        Move();
    }

    void Move()
    {
        if (isStart && !isStop)
            rigid.MovePosition(transform.position + transform.rotation * Vector3.forward.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine("WaitStart");
        }
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2.0f);
        isStart = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="BossZone")
        {
            isStop = true;
            isStart = false;
        }
    }

    void ResetPos()
    {
        if (isStart == true && player.IsPlayerFall)
        {
            isStart = false;
            transform.position = startPos;
        }
    }
}
