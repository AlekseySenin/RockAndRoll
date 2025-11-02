using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _levelIndex;


    private void Awake()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();

    }
    private void OnButtonClicked()
    {
        if (SimpleSceneLoader.Instance == null)
        {
            Debug.LogError("[LoadSceneButton] SimpleSceneLoader.Instance is null!");
            return;
        }

        SimpleSceneLoader.Instance.LoadScene(_levelIndex);
    }
}
