using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyColorController : MonoBehaviour
{
    [SerializeField] Joystick masterJoystick;
    [SerializeField] Joystick slaveJoystick;
    [SerializeField] List<UnityEngine.UI.Image> images;
    [SerializeField] Color trueColor, falseColor;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        slaveJoystick.enabled = masterJoystick.isPressed;
        foreach (var item in images)
        {
            item.color = masterJoystick.isPressed ? trueColor : falseColor;
        }
    }
}
