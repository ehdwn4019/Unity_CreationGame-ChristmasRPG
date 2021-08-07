using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Transform camLookPos;
    [SerializeField]
    float camSpeed;

    float camX;
    float camY;
    float camZ;
    Vector3 camStopPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAround();
    }

    
    void Move()
    {
        // 플레이어 방향과 카메라 방향 일치시키기
        Vector3 forward = new Vector3(camLookPos.forward.x, 0f, camLookPos.forward.z).normalized;
        target.rotation = Quaternion.Lerp(transform.rotation, camLookPos.rotation, camSpeed * Time.deltaTime);
        target.forward = forward;
    }

    //카메라 회전
    void LookAround()
    {
        if (Input.GetMouseButton(1))
        {
            camX += Input.GetAxis("Mouse X");
            camY += Input.GetAxis("Mouse Y") * -1;

            camY = Mathf.Clamp(camY, -2f, 2f);

            Vector3 camPos = new Vector3(camLookPos.rotation.x + camY, camLookPos.rotation.y + camX, 0) * camSpeed;

            camLookPos.rotation = Quaternion.Euler(camPos);
            camStopPos = camLookPos.eulerAngles;
        }
        else
        {
            camLookPos.rotation = Quaternion.Euler(camStopPos);
        }
    }
}
