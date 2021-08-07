using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public static MonsterSpawn instance =null;

    [SerializeField]
    GameObject[] responePos;

    GameObject monster;

    //public Queue<GameObject> objectPooling = new Queue<GameObject>();

    public List<GameObject> monsterPooling = new List<GameObject>(); // 오브젝트 풀링 List

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //CreateMonster();

        //InvokeRepeating("TakeOut", 6, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //random = Random.Range(1, objectPooling.Count);
        
    }

    //몬스터 미리 생성 
    public void CreateMonster()
    {
        for (int i=0; i<responePos.Length; i++)
        {
            GameObject mon = ObjectPoolingManager.Create("Monster", responePos[i].transform.position, Quaternion.Euler(0,180f,0));
            monsterPooling.Add(mon);
            //objectPooing.Add(mon);
            //mon.SetActive(true);
        }
    }
    
    //몬스터 넣고 비활성화 
    public void Disappear(GameObject monster)
    {
        //monsterPooling.Add(monster);
        //monster.SetActive(false);
        ObjectPoolingManager.Put(monster, monsterPooling);
    }
    
    //몬스터 꺼내고 활성화 및 위치 세팅
    public GameObject Appear()
    {
        Vector3 randomPos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        //for(int i=0; i< monsterPooling.Count; i++)
        //{
        //    if(!monsterPooling[i].activeInHierarchy)
        //    {
        //        monsterPooling[i].SetActive(true);
        //        monsterPooling[i].transform.position = responePos[i].transform.position + randomPos;
        //        return monsterPooling[i];
        //    }
        //}
        //
        //return null;

        return ObjectPoolingManager.TakeOut(monsterPooling, randomPos);
    }

    //public void CreateMonster()
    //{
    //    for(int i=0; i<responePos.Length; i++)
    //    {
    //        GameObject mon = Instantiate(monster, responePos[i].transform.position, monster.transform.rotation);
    //        objectPooling.Enqueue(mon);
    //        mon.SetActive(true);
    //    }
    //}
    //
    //public void Put(GameObject monster)
    //{
    //    Debug.Log("put");
    //    objectPooling.Enqueue(monster);
    //    monster.SetActive(false);
    //}
    //
    //public GameObject TakeOut()
    //{
    //    Debug.Log("TakeOut");
    //
    //    int random = Random.Range(0, responePos.Length-1);
    //    Vector3 randomPos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
    //    GameObject mon = objectPooling.Dequeue();
    //    mon.SetActive(true);
    //    mon.transform.position = responePos[random].transform.position + randomPos;
    //    //아니다 싶으면 while 문 날리기
    //    //while (true)
    //    //{
    //    //    if(objectPooling.Count==0)
    //    //    {
    //    //        break;
    //    //    }
    //    //    else
    //    //    {
    //    //        mon = objectPooling.Dequeue();
    //    //        mon.SetActive(true);
    //    //        mon.transform.position = responePos[random].transform.position+ randomPos;
    //    //        
    //    //    }
    //    //}
    //
    //    return mon;
    //}
}
