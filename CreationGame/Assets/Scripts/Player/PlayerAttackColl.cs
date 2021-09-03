using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColl : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("Disappear");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().DecreaseHP(Random.Range(3,7));
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
