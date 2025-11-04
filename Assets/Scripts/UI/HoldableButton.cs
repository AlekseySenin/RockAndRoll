using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldableButton : Button, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressed { get; private set; }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        IsPressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        IsPressed = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        IsPressed = false;
    }
}