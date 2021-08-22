using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Obstacles
{
    [SerializeField]
    Transform target;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float bulletPower;

    bool isShoot = true;

    protected override void Init()
    {
        base.Init();
        ot = ObstaclesType.Cannon;
    }

    protected override void Loop()
    {
        base.Loop();

        LookTarget();
        Shoot();
    }

    void LookTarget()
    {
        //Vector3 forward = target.position - transform.position;
        Vector3 forward = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(forward);
        //Quaternion quaternion = Quaternion.LookRotation(forward);
        //transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, Time.deltaTime * 5.0f);
    }

    void Shoot()
    {
        if(isShoot)
        {
            Debug.Log("TEST");
            
        }
        if(isShoot)
        {
            StartCoroutine("test");
        }
        //CannonBulletSpawn.instance.CreateBullet();
    }

    IEnumerator test()
    {
        CannonBulletSpawn.instance.CreateBullet();
        isShoot = false;
        yield return new WaitForSeconds(5.0f);
        isShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            isShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            isShoot = false;
        }
    }
}
