using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlockSpawn : MonoBehaviour
{
    public static FallBlockSpawn instance = null;

    [SerializeField]
    GameObject[] responePos;

    List<GameObject> blockPooling = new List<GameObject>();

    private void Start()
    {
        //testPos = GameObject.Find("TestPos");
        //fallBlock = ObjectPoolingManager.instance.Create("FallBlo ck", transform.position, Quaternion.identity);
        //fallBlock = Instantiate(Resources.Load("Prefab/FallBlock"), testPos.transform.position, Quaternion.identity) as GameObject;

        //for(int i=0; i<responePos.Length; i++)
        //{
        //    GameObject go = ObjectPoolingManager.Create("FallBlock", responePos[i].transform.position, Quaternion.identity);
        //    //GameObject go = Instantiate(Resources.Load("Prefab/FallBlock"), responePos[i].transform.position, Quaternion.identity) as GameObject;
        //}

        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateBlock()
    {
        for(int i=0; i<responePos.Length; i++)
        {
            GameObject block = ObjectPoolingManager.Create("FallBlock", responePos[i].transform.position, Quaternion.identity);
            blockPooling.Add(block);
        }
    }

    public void Disappear(GameObject block)
    {
        ObjectPoolingManager.Put(block, blockPooling);
    }

    public GameObject Appear(Vector3 startPos)
    {
        return ObjectPoolingManager.TakeOut(blockPooling,startPos);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        for(int i=0; i< blockPooling.Count; i++)
    //        {
    //            Disappear(blockPooling[i]);
    //        }
    //    }
    //}
    
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "DeadZone")
    //    {
    //        GameObject block = Appear();
    //        //gameObject.SetActive(false);
    //
    //    }
    //}

    //protected override void Init()
    //{
    //    base.Init();
    //
    //    testPos = GameObject.Find("TestPos");
    //    //GameObject test = Resources.Load("Prefab/FallBlock") as GameObject;
    //    //    Instantiate(test, testPos.transform.position, Quaternion.identity);
    //
    //    fallBlock = ObjectPoolingManager.instance.Create("FallBlock", testPos.transform.position, Quaternion.identity);
    //    //rigid = fallBlock.GetComponent<Rigidbody>();
    //    //rigid.useGravity = false;
    //    //startPos = transform.position;
    //    //
    //    //
    //    //Destroy(fallBlock, 0.1f);
    //
    //}

    //protected override void Loop()
    //{
    //    base.Loop();
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        rigid.useGravity = true;
    //        rigid.AddForce(Vector3.down * 10.0f);
    //        canFalling = false;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.name == "DeadZone")
    //    {
    //        StartCoroutine(ReturnPos());
    //    }
    //}
    //
    //IEnumerator ReturnPos()
    //{
    //    //GameObject go = Instantiate(Resources.Load("Prefab/FallBlock"),startPos,Quaternion.identity) as GameObject;
    //   // rigid.useGravity = false;
    //    yield return new WaitForSeconds(3.0f);
    //    transform.position = startPos;
    //    canFalling = true;
    //}
}
