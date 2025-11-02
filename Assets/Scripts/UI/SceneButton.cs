using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private List<Image> gears;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color colectedColor;
    [SerializeField] private Text indexText;
    [SerializeField] private GameObject Content;
    [SerializeField] private int index;
    private SceneData sceneData;

    private void Start()
    {
        button.onClick.AddListener(Press);
    }

    private void Press()
    {
        WorldSceneManager.OnSceneOpen(sceneData);
        SceneManager.LoadScene(sceneData.sceneIndex);
    }

    public void Setup(SceneData data)
    {
        sceneData = data;
        sceneData.relativeSceneIndex = index;
        indexText.text = sceneData.sceneIndex.ToString();
        if (sceneData.isOpened)
        {
            button.interactable = true;

            for (int i = 0; i < gears.Count; i++)
            {
                if (i < sceneData.gearsToColect)
                {
                    gears[i].gameObject.SetActive(true);
                    gears[i].color = sceneData.gearsColected > i ? colectedColor : regularColor;
                }
                else
                {
                    gears[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            {
                button.interactable = false;

                for (int i = 0; i < gears.Count; i++)
                {
                    gears[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void Show(SceneData data)
    {
        Content.SetActive(true);
        Setup(data);
    }

    public void Hide()
    {
        Content.SetActive(false);
    }
}
