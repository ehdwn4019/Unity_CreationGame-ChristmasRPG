using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public static MonsterSpawn instance =null;

    [SerializeField]
    GameObject[] responePos;

    //몬스터 부모 오브젝트
    [SerializeField]
    GameObject monsters;

    GameObject monster;

    // 오브젝트 풀링 List
    public List<GameObject> monsterPooling = new List<GameObject>(); 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    //몬스터 미리 생성 
    public void CreateMonster()
    {
        for (int i=0; i<responePos.Length; i++)
        {
            GameObject mon = ObjectPoolingManager.Create("Monster", responePos[i].transform.position, Quaternion.Euler(0,180f,0));
            mon.transform.SetParent(monsters.transform); // 부모 오브젝트에 자식으로 세팅
            monsterPooling.Add(mon);
        }
    }
    
    //몬스터 넣고 비활성화 
    public void Disappear(GameObject monster)
    {
        ObjectPoolingManager.Put(monster, monsterPooling);
    }
    
    //몬스터 꺼내고 활성화 및 위치 세팅
    public GameObject Appear(Vector3 startPos)
    {
        //랜덤 위치
        Vector3 randomPos = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        return ObjectPoolingManager.TakeOut(monsterPooling, startPos+randomPos);
    }
}
