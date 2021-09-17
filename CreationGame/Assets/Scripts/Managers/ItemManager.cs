using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    [SerializeField]
    GameObject[] fieldItem;
    
    public List<Item> itemData = new List<Item>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InsertData();
    }

    private void Update()
    {
        Debug.Log("length" + fieldItem.Length);
    }

    void InsertData()
    {
        if(fieldItem != null)
        {
            for (int i = 0; i<fieldItem.Length; i++)
            {
                
                fieldItem[i].GetComponent<FieldItem>().SetItem(itemData[i]);
            }
        }
        
    }
}
