using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementButtonsController : MonoBehaviour
{

    [SerializeField] Button _upButton;
    [SerializeField] Button _downButton;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;
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
        Debug.Log("h:" + Horizontal);
        Debug.Log("v:" + Vertical);
        Debug.Log("j:" + Jump);
    }

    void Move(float val) 
    {        

    }

    void Scale(float val) 
    {
        
    }

    void DoJump(bool val)
    {

    }
}
