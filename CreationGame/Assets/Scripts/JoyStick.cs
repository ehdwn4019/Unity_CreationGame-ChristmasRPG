using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    public RectTransform BG;
    public RectTransform stick;

    Vector3 startPos;
    Vector2 value;
    float radius;
    public bool isTouch;

    public Vector2 Value { get { return value; } }

    // Start is called before the first frame update
    void Start()
    {
        startPos = stick.transform.position;
        //반지름 구하기
        radius = BG.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnDrag(PointerEventData eventData)
    {
        //마우스 좌표에서 조이스틱 백그라운드 이미지의 좌표 빼기 
        value = eventData.position - (Vector2)BG.position;

        value = Vector2.ClampMagnitude(value, radius);

        //부모 좌표에서 거리 벌리기
        stick.localPosition = value;

        value = value.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        stick.position = startPos;
    }
}
