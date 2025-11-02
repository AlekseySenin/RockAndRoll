using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectWindow : MonoBehaviour
{
    [SerializeField] private Button prevChapterButton;
    [SerializeField] private Button nextChapterButton;
    [SerializeField] private TextMeshProUGUI ChapterNameTMP;
    [SerializeField] private List<SceneButton> sceneButtons;
    public int selectedChapter;

    private void Start()
    {
        GameController.OnMainMenuOpened?.Invoke();
        selectedChapter = WorldSceneManager.Instance.CurrentSceneData().chapterIndex;
        ShowChapter();
        prevChapterButton.onClick.AddListener(pressPrev);
        nextChapterButton.onClick.AddListener(pressNext);
    }

    private void pressPrev()
    {
        selectedChapter = selectedChapter > 1 ? selectedChapter - 1 : WorldSceneManager.Instance.NumberOfChapters();
        ShowChapter();
    }

    private void pressNext()
    {
        selectedChapter = selectedChapter < WorldSceneManager.Instance.NumberOfChapters() ? selectedChapter+1 : 1;
        ShowChapter();
    }

    private void ShowChapter()
    {
        List<SceneData> sceneDatas = WorldSceneManager.Instance.ChaptersData(selectedChapter);
        for (int i = 0; i < sceneButtons.Count; i++)
        {
            if (i<sceneDatas.Count)
            {
                sceneButtons[i].Show(sceneDatas[i]);
            }
            else
            {
                sceneButtons[i].Hide();
            }
        }
        ChapterNameTMP.text = "Chapter: " + selectedChapter;
    }
}
