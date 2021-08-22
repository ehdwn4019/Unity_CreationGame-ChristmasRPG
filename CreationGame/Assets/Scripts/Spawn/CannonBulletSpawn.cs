using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletSpawn : MonoBehaviour
{
    public static CannonBulletSpawn instance = null;

    [SerializeField]
    GameObject responePos;

    List<GameObject> bulletPooling = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateBullet()
    {
        GameObject bullet = ObjectPoolingManager.Create("CannonBullet", responePos.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(-4.0f,0f,0f) * 25.0f, ForceMode.Impulse);
        bulletPooling.Add(bullet);
    }

    public void Disappear(GameObject bullet)
    {
        ObjectPoolingManager.Put(bullet, bulletPooling);
    }

    public GameObject Appear(Vector3 startPos)
    {
        return ObjectPoolingManager.TakeOut(bulletPooling, startPos);
    }
}
