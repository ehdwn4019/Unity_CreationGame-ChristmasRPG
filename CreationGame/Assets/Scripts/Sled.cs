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
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        booster = false;
        coll = GetComponents<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
        //if(booster)
        //    transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            booster = true;
        
            //for(int i=0; i<coll.Length; i++)
            //{
            //    coll[i].enabled = true;
            //}
        }
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
