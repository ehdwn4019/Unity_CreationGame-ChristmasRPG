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

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.V))
        //    nav.destination = target.position;

        //FindStation();

        //if(isStart && !isStop)
        //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
        

        Debug.Log(player.IsPlayerFall);
        Debug.Log("isStart : "+isStart);
        Debug.Log("isStop : "+isStop);
    }

    private void FixedUpdate()
    {
        ResetPos();
        Move();
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        StartCoroutine("WaitStart");
    //    }
    //}

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
        yield return new WaitForSeconds(3.0f);
    
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
            transform.position = startPos;
            isStart = false;
            isStop = false;

            //isStart = false;
        }
    }

    // TODO : 상태에 따라서 가야할길 분류하기 
    //void FindStation()
    //{
    //    //nav.SetDestination(direction.position);
    //    
    //    if (isStop)
    //        StartCoroutine("Turn");
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if(other.gameObject.name == "RotatePos")
    //    //{
    //    //    isStop = true;
    //    //}
    //}

    //IEnumerator Turn()
    //{
    //    //nav.ResetPath();
    //    //nav.speed = 0;
    //    nav.destination = Vector3.zero;
    //    yield return new WaitForSeconds(0.5f);
    //    nav.stoppingDistance = 2f;
    //    Vector3 dir =  target.position - transform.position;
    //    Quaternion rotation = Quaternion.LookRotation(dir);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 8.0f * Time.deltaTime);
    //    yield return new WaitForSeconds(0.5f);
    //    //nav.speed = 5.0f;
    //    nav.destination = target.position;
    //    //nav.SetDestination(target.position);
    //    //nav.SetPath();
    //    //
    //    //isStop = false;
    //    //nav.SetPath();
    //    //    nav.SetPath(NavMesh.);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Stop")
    //    {
    //        Debug.Log("GG");
    //        booster = false;
    //    }
    //}
}
