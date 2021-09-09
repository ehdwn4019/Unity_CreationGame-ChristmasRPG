using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickRotate : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
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
        startPos = stick.transform.position;

        radius = bg.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        value = eventData.position - (Vector2)bg.position;

        value = Vector2.ClampMagnitude(value, radius);

        stick.localPosition = value;

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
