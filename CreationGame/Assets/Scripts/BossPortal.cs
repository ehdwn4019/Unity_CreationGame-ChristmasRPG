using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    [SerializeField]
    GameObject princess;

    [SerializeField]
    GameObject returnPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            SoundManager.instance.PlaySoundBgm("게임");
            princess.transform.position = returnPos.transform.position;
            princess.transform.rotation = returnPos.transform.rotation;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
