using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFallBlock : MonoBehaviour
{
    Rigidbody rigid;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "DeadZone")
        {
            StartCoroutine("ComeBack");
        }
    }

    IEnumerator ComeBack()
    {
        gameObject.SetActive(false);
        transform.position = startPos;
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(true);
    }
}
