using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlockSpawn : MonoBehaviour
{
    public static FallBlockSpawn instance = null;

    [SerializeField]
    GameObject[] responePos;

    List<GameObject> blockPooling = new List<GameObject>();

    [SerializeField]
    GameObject fallBlocks;

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

    public void CreateBlock()
    {
        for(int i=0; i<responePos.Length; i++)
        {
            GameObject block = ObjectPoolingManager.Create("FallBlock", responePos[i].transform.position, Quaternion.identity);
            block.transform.SetParent(fallBlocks.transform);
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
}
