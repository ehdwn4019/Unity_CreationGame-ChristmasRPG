using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sled : MonoBehaviour
{
    [SerializeField]
    float speed;

    bool booster;
    BoxCollider[] coll;

    NavMeshAgent nav;

    [SerializeField]
    Transform direction;

    [SerializeField]
    Transform target;

    bool isStop;

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //booster = false;
        coll = GetComponents<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();

        nav.destination = direction.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindStation();
        
        //if(booster)
        //    transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        booster = true;
    //    
    //        //for(int i=0; i<coll.Length; i++)
    //        //{
    //        //    coll[i].enabled = true;
    //        //}
    //    }
    //}

    // TODO : 상태에 따라서 가야할길 분류하기 
    void FindStation()
    {
        //nav.SetDestination(direction.position);
        
        if (isStop)
            StartCoroutine("Turn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RotatePos")
        {
            isStop = true;
        }
    }

    IEnumerator Turn()
    {
        Debug.Log("in");
        //nav.ResetPath();
        //nav.speed = 0;
        nav.destination = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        nav.stoppingDistance = 2f;
        Vector3 dir =  target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 8.0f * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        //nav.speed = 5.0f;
        nav.destination = target.position;
        //nav.SetDestination(target.position);
        //nav.SetPath();
        //
        //isStop = false;
        //nav.SetPath();
        //    nav.SetPath(NavMesh.);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Stop")
    //    {
    //        Debug.Log("GG");
    //        booster = false;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.name =="Player")
    //    {
    //        for(int i=0; i<coll.Length; i++)
    //        {
    //            coll[i].enabled = false;
    //        }
    //    }
    //}
}
