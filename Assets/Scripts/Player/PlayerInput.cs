using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] LayerMask layerMask;

    private bool ScreenPressed;
    private Vector2 TouchPosition;
    private Vector2 TouchStartPosition;
    private bool charSelected;
    private float vertical, horizontal;

    public static InputType InputType = InputType.ButtonControl;//TapOnScreenControl;

    private void Update()
    {
        switch (InputType)
        {
            case InputType.TapOnScreenControl:
                TapOnScreenControl();
                break;
            case InputType.TapOnCharControl:
                TapOnCharControl();
                break;
            case InputType.JoyControl:
                JoyControl();
                break;
            case InputType.ButtonControl:
                ButtonControl();
                break;
            default:
                break;
        }
    }

    private void TapOnScreenControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(Input.GetTouch(0).position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TouchStartPosition = Input.GetTouch(0).position;
                    TouchPosition = Input.GetTouch(0).position;
                    Debug.Log(TouchStartPosition);
                    player.InpupReceived(TouchStartPosition - TouchPosition, true);
                    break;
                case TouchPhase.Moved:
                    TouchPosition = Input.GetTouch(0).position; ;
                    player.InpupReceived(TouchStartPosition - TouchPosition, true);
                    break;
                case TouchPhase.Stationary:
                    TouchPosition = Input.GetTouch(0).position;
                    player.InpupReceived(TouchStartPosition - TouchPosition, true);
                    break;
                case TouchPhase.Ended:
                    TouchPosition = Input.GetTouch(0).position;
                    player.InpupReceived(TouchStartPosition - TouchPosition, false);
                    break;
                case TouchPhase.Canceled:
                    TouchPosition = Input.GetTouch(0).position;
                    player.InpupReceived(TouchStartPosition - TouchPosition, false);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchStartPosition = Input.mousePosition;
                TouchPosition = Input.mousePosition;
                player.InpupReceived(TouchStartPosition - TouchPosition, true);
            }
            if (Input.GetMouseButton(0))
            {
                TouchPosition = Input.mousePosition;
                player.InpupReceived(TouchStartPosition - TouchPosition, true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                player.InpupReceived(TouchStartPosition - TouchPosition, false);
            }
        }
    }

    private void TapOnCharControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(Input.GetTouch(0).position);
            switch (touch.phase)
            {
                case TouchPhase.Began:


                      Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit2D hit;
                if (!Camera.main.orthographic)
                {
                    hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);
                }
                else
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero, layerMask);
                }

                if (hit.collider != null)
                {
                    foreach (var item in player.chars)
                    {
                        if (hit.collider.gameObject == item.gameObject)
                        {
                            player.SelectCharacter(item);
                            TouchStartPosition = Input.GetTouch(0).position;
                            TouchStartPosition = Input.GetTouch(0).position;
                            TouchPosition = Input.GetTouch(0).position;
                            player.InpupReceived(hit.point - TouchPosition, true);
                            charSelected = true;
                        }
                    }
                }
            
                    break;
                case TouchPhase.Moved:
                    TouchPosition = Input.GetTouch(0).position; ;
                    player.InpupReceived(TouchStartPosition - TouchPosition, true);
                    break;
                case TouchPhase.Stationary:
                    TouchPosition = Input.GetTouch(0).position; ;
                    player.InpupReceived(TouchStartPosition - TouchPosition, true);
                    break;
                case TouchPhase.Ended:
                    TouchPosition = Input.GetTouch(0).position;
                    player.InpupReceived(TouchStartPosition - TouchPosition, false);
                    break;
                case TouchPhase.Canceled:
                    TouchPosition = Input.GetTouch(0).position;
                    player.InpupReceived(TouchStartPosition - TouchPosition, false);
                    break;
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit;
                if (!Camera.main.orthographic)
                {
                    hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);
                }
                else
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero, layerMask);
                }

                if (hit.collider != null)
                {
                    foreach (var item in player.chars)
                    {
                        if (hit.collider.gameObject == item.gameObject)
                        {
                            player.SelectCharacter(item);
                            TouchStartPosition = Input.mousePosition;
                            TouchStartPosition = Input.mousePosition;
                            TouchPosition = Input.mousePosition;
                            player.InpupReceived(hit.point - TouchPosition, true);
                            charSelected = true;
                        }
                    }
                }
            }
            if (Input.GetMouseButton(0)&& charSelected)
            {
                TouchPosition = Input.mousePosition;
                player.InpupReceived(TouchStartPosition - TouchPosition, true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                charSelected = false;
                player.InpupReceived(TouchStartPosition - TouchPosition, false);
            }
        }
    }

    private void JoyControl()
    {
        Vector2 vector = new Vector2(UIManager.Horizontal, UIManager.Vertical)*1000;
        player.InpupReceived(vector, vector.magnitude>0.1);
    } 

    private void ButtonControl()
    {
        Vector2 vector = new Vector2(UIManager.Horizontal, UIManager.Vertical) * -1000;
        Debug.Log(vector);

        player.InpupReceived(vector, vector.magnitude > 0.1);
    }
}

[System.Serializable]
public enum InputType
{
    TapOnScreenControl, TapOnCharControl, JoyControl, ButtonControl
}