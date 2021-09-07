using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sled : MonoBehaviour
{
    [SerializeField]
    float speed;

    bool isStart;
    bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.V))
        //    nav.destination = target.position;

        //FindStation();
        if(isStart && !isStop)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        yield return new WaitForSeconds(4.0f);

        isStart = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="BossZone")
        {
            isStart = true;
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
