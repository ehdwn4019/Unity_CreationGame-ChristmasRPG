using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMove : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField]
    RectTransform bg;

    [SerializeField]
    RectTransform stick;

    Vector3 startPos;
    Vector2 value;
    float radius;
    public bool isTouch;

    public Vector2 Value { get { return value; } }

    // Start is called before the first frame update
    void Start()
    {
        //시작위치 저장
        startPos = stick.transform.position;

        //반지름 구하기
        radius = bg.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnDrag(PointerEventData eventData)
    {
        //마우스 좌표에서 조이스틱 백그라운드 이미지의 좌표 빼기 
        value = eventData.position - (Vector2)bg.position;

        //벡터 최대길이 제한하기
        value = Vector2.ClampMagnitude(value, radius);

        //부모 좌표에서 거리 벌리기
        stick.localPosition = value;

        //value값 정규화
        value = value.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (Input.GetMouseButton(1))
        //    return;

        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        stick.position = startPos;
    }
}
