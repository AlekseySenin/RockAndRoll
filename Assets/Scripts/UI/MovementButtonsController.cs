using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementButtonsController : MonoBehaviour
{
    [SerializeField] HoldableButton _upButton;
    [SerializeField] HoldableButton _downButton;
    [SerializeField] HoldableButton _leftButton;
    [SerializeField] HoldableButton _rightButton;
    [SerializeField] HoldableButton _jumpButton;
    public static MovementButtonsController Instance;
    public float Horizontal;
    public float Vertical;
    public bool Jump;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        CheckHorizontal();
        CheckVertical();
        CheckJump();
    }

    void CheckVertical()
    {
        bool a = _upButton.IsPressed;
        bool b = _downButton.IsPressed;
        if (a && b)
        {
            Vertical = 0;
            return;
        }
        if (!a && !b)
        {
            Vertical = 0;
            return;
        }
        if (a)
        {
            Vertical = 1;
            return;
        }
        if (b)
        {
            Vertical = -1;
            return;
        }
    }

    void CheckHorizontal()
    {
        bool a = _leftButton.IsPressed;
        bool b = _rightButton.IsPressed;
        if (a && b)
        {
            Horizontal = 0;
            return;
        }
        if (!a && !b)
        {
            Horizontal = 0;
            return;
        }
        if (a)
        {
            Horizontal = 1;
            return;
        }
        if (b)
        {
            Horizontal = -1;
            return;
        }
    }

    void CheckJump()
    {
        Jump = _jumpButton.IsPressed;
    }
}
