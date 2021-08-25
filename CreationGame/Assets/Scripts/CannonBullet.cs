using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;

    Rigidbody rigid;
    GameObject startPos;

    private void Awake()
    {
        startPos = GameObject.Find("ShootPos");
    }

    private void OnEnable()
    {
        if(rigid == null)
            rigid = gameObject.AddComponent<Rigidbody>();
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        transform.rotation = startPos.transform.rotation;
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        rigid.AddForce(Vector3.left * bulletSpeed, ForceMode.Impulse);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * bulletSpeed, ForceMode.Impulse);
            CannonBulletSpawn.instance.Disappear(gameObject);
            Destroy(rigid);
            Invoke("ReturnPos", 2.0f);
            Debug.Log("플레이어와 충돌");
        }

        if(other.gameObject.name == "CannonBulletStopZone")
        {
            CannonBulletSpawn.instance.Disappear(gameObject);
            Destroy(rigid);
            Invoke("ReturnPos", 2.0f);
        }
    }
    
    void ReturnPos()
    {
        
        CannonBulletSpawn.instance.Appear(startPos.transform.position);
    }
}
