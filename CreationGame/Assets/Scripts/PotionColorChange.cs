using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionColorChange : MonoBehaviour
{
    [SerializeField]
    float speed;

    Image img;
    float r = 255.0f;
    float g = 1.0f;
    float b = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //img.color = new Color(r/255, g/255, b/255);
        img.color = new Color(r/255, g/255, b/255);

        //Debug.Log("r : " + r  + "  g : " + g  + "  b : " + b );
        Change();
    }

    void Change()
    {
        if(g<=255.0f && b<=2.0f)
        {
            //Debug.Log("1");
            g += speed * Time.deltaTime;
        }
        
        if(r>=0 && g>255.0f) 
        {
            //Debug.Log("2");
            r -= speed * Time.deltaTime;
        }
        
        if(b<=255.0f && r<0)
        {
            //Debug.Log("3");
            b += speed * Time.deltaTime;
        }

        if(g>0 && b>255.0f)
        {
            //Debug.Log("4");
            g -= speed * Time.deltaTime;
        }
        
        if(r<=255.0f && g<0)
        {
            //Debug.Log("5");
            r += speed * Time.deltaTime;
        }
        
        if(b>=0 && r>255.0f)
        {
            //Debug.Log("6");
            b -= speed * Time.deltaTime;
        }


    }
}
