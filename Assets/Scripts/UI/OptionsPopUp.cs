using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopUp : MonoBehaviour
{
    //[SerializeField] Button movemrntTypeBtn;
    [SerializeField] Button inputTypeBtn;
    [SerializeField] Button cameraTypeBtn;
    [SerializeField] Button CloseBtn;
   // [SerializeField] Text movemrntTypeText;
    [SerializeField] Text inputTypeText;
    [SerializeField] Text cameraTypeText;
    [SerializeField] Text moveSpeedText;
    [SerializeField] Text strachSpeedText;
    [SerializeField] Slider scaleSlider;
    [SerializeField] Slider turnSlider;

    private void Awake()
    {
        inputTypeBtn.onClick.AddListener(PressInputTypeBtn);
        cameraTypeBtn.onClick.AddListener(PressCameraTypeBtn);
        CloseBtn.onClick.AddListener(Hide);
        //scaleSlider.onValueChanged.AddListener((x) => { Player.scaleSpeed = x; strachSpeedText.text = "Hand strach speed: " + Player.scaleSpeed; });
        //turnSlider.onValueChanged.AddListener((x) => { Player.turnSpeed = x; moveSpeedText.text = "Movement speed: " + Player.turnSpeed; });
    }

    public static Action OnCameraTarhetChsnged;

    void PressInputTypeBtn()
    {
        switch (PlayerInput.InputType)
        {
            case InputType.TapOnScreenControl:
                PlayerInput.InputType = InputType.JoyControl;
                Player.continueMoveing = true;

                break;
            case InputType.TapOnCharControl:
                PlayerInput.InputType = InputType.TapOnScreenControl;
                Player.continueMoveing = true;



                break;
            case InputType.JoyControl:
                PlayerInput.InputType = InputType.ButtonControl;
                Player.continueMoveing = true;


                break;

            case InputType.ButtonControl:
                PlayerInput.InputType = InputType.TapOnCharControl;
                Player.continueMoveing = false;


                break;
            default:
                break;
        }
        Setup();
    }

    public void PressCameraTypeBtn()
    {
        OnCameraTarhetChsnged?.Invoke();
        Setup();
    }

    public void Setup()
    {
        Time.timeScale = 0;
        inputTypeText.text = PlayerInput.InputType.ToString();
        cameraTypeText.text = CameraMovement.TargetName;
        scaleSlider.value = Player.scaleSpeed;
        turnSlider.value = Player.TurnSpeed;
        moveSpeedText.text = "Movement speed: " + Player.TurnSpeed;
        strachSpeedText.text = "Hand strach speed: " + Player.scaleSpeed;
        UIManager.Instance.Joys.SetActive(PlayerInput.InputType == InputType.JoyControl);
        UIManager.Instance.Buttons.SetActive(PlayerInput.InputType == InputType.ButtonControl);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
