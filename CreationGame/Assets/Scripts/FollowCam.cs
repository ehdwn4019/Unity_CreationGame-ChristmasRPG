using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    Transform camLookPos;

    JoyStickRotate joyStickRotate;
    JoyStickMove joyStickMove;
    Vector3 camStopPos;

    [SerializeField]
    float camSpeed;

    float camX;
    float camY;
    float camZ;

    // Start is called before the first frame update
    void Start()
    {
        joyStickRotate = GameObject.FindObjectOfType<JoyStickRotate>();
        joyStickMove = GameObject.FindObjectOfType<JoyStickMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        LookAround();
    }

    //플레이어 회전
    void Rotate()
    {
        if (target.GetComponent<Player>().IsDie)
            return;

        // 플레이어 방향과 카메라 방향 일치시키기
        Vector3 forward = new Vector3(camLookPos.forward.x, 0f, camLookPos.forward.z).normalized;

        //Quaternion quaternion = Quaternion.Euler(transform.rotation.eulerAngles);
        //target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(quaternion, camLookPos.rotation, camSpeed * Time.fixedDeltaTime));
        //target.GetComponent<Rigidbody>().rotation *= Quaternion.Lerp(transform.rotation, camLookPos.rotation, camSpeed * Time.fixedDeltaTime);
        //target.rotation = Quaternion.Lerp(transform.rotation, camLookPos.rotation, camSpeed * Time.deltaTime);
        target.Rotate(Quaternion.Lerp(transform.rotation, camLookPos.rotation, camSpeed * Time.deltaTime).eulerAngles);
        target.forward = forward;
    }

    //카메라 회전
    void LookAround()
    {
        // 나중에 이거 지우고 빌드해서 테스트해보기 동시터치 될수도 
        if((joyStickRotate.isTouch && Input.GetMouseButton(1)) || (joyStickMove.isTouch && Input.GetMouseButton(1))) 
        {
            return;
        }

        //컴퓨터 모드 
        if (GameManager.instance.ct == GameManager.ControllType.Computer)
        {
            if (Input.GetMouseButton(1))
            {
                camX += Input.GetAxis("Mouse X");
                camY += Input.GetAxis("Mouse Y") * -1;

                //카메라 Y값 제한 
                camY = Mathf.Clamp(camY, -3.5f, 2f);

                Vector3 camPos = new Vector3(camLookPos.rotation.x + camY, camLookPos.rotation.y + camX, 0) * camSpeed;

                camLookPos.rotation = Quaternion.Euler(camPos);
                camStopPos = camLookPos.eulerAngles;
            }
            else
            {
                camLookPos.rotation = Quaternion.Euler(camStopPos);
            }
        }
        else
        {
            if(joyStickRotate.isTouch)
            {
                camX += joyStickRotate.Value.x;
                camY += joyStickRotate.Value.y * -1;

                camY = Mathf.Clamp(camY, -3.5f, 2f);

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
}
