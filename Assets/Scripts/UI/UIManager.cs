using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private PopUp winPopUp;
    [SerializeField] private PopUp losePopUp;
    [SerializeField] private OptionsPopUp optionsPopUp;
    [SerializeField] Slider levelProgress;
    [SerializeField] Slider fireProgress;
    [SerializeField] Slider TimerSlider;
    [SerializeField] TextMeshProUGUI LevelNumber;
    [SerializeField] Button OptionsBtn;
    [SerializeField] Joystick horizontalJoy;
    [SerializeField] Joystick verticalJoy;
    [SerializeField] Canvas canvas;
    public GameObject Joys, Buttons;
    public static float Vertical;
    public static float Horizontal;
    public static bool Jump;

    private void Awake()
    {
        canvas.enabled = false;
        Instance = this;
        DontDestroyOnLoad(transform.parent.gameObject);
        GameController.OnGameStart += Show;
        GameController.OnGameStart += HidePopUps;
        WorldSceneManager.OnSceneOpen += SetLevelText;
        GameController.OnGameLose += ShowLosePpUp;
        GameController.OnGameWin += ShowWinPopUp;
        GameController.OnMainMenuOpened += HidePopUps;
        OptionsBtn.onClick.AddListener(ShowOptionsPopup);
    }

    private void HidePopUps()
    {
        winPopUp.gameObject.SetActive(false);
        losePopUp.gameObject.SetActive(false);
    }

    public void MoveHorizontal(float val)
    {
        Horizontal = val;
    }

    public void MoveVertical(float val)
    {
        Vertical = val;
    }

    private void Show()
    {
        canvas.enabled = true;
    }



    private void SetLevelText(SceneData sceneData)
    {
        LevelNumber.text = "World " + sceneData.chapterIndex + " level" + sceneData.relativeSceneIndex;
    }
    private void Update()
    {
        if (PlayerInput.InputType == InputType.JoyControl)
        {
            Vertical = verticalJoy.Vertical;
            Horizontal = horizontalJoy.Horizontal;
        }
    }

    private void OnDestroy()
    {
        GameController.OnGameStart -= Show;
        GameController.OnGameStart -= HidePopUps;
        WorldSceneManager.OnSceneOpen -= SetLevelText;
        GameController.OnGameLose -= ShowLosePpUp;
        GameController.OnGameWin -= ShowWinPopUp;
        GameController.OnMainMenuOpened -= HidePopUps;
    }

    public void ShowWinPopUp()
    {
        winPopUp.gameObject.SetActive(true);
    }

    public void ShowLosePpUp()
    {
        losePopUp.gameObject.SetActive(true);
    }

    public void ShowOptionsPopup()
    {
        optionsPopUp.gameObject.SetActive(true);
        optionsPopUp.Setup();

    }
}
