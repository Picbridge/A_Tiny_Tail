using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject CertQuitPanel;
    public GameObject CertLossDataPanel;
    public GameObject optionPanel;

    private void Start()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.ChooseBGM(0);
        SoundManager.instance.PlayBGM();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionPanel.activeSelf)
                optionPanel.SetActive(false);
            else if (CertQuitPanel.activeSelf)
                CertQuitPanel.SetActive(false);
            else
                QuitGame();
        }
    }

    public void StartGame()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void Option()
    {
        SoundManager.instance.PlaySound("ButtonSound");
    }

    public void ResetData()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        CertLossDataPanel.SetActive(true);
    }

    public void ResetData_Yes()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        SaveLoadManager.instance.Delete();
        CharacterSelectionManager.instance.ResetData();
        RankingManager.instance.ResetData();
        CertLossDataPanel.SetActive(false);
    }

    public void ResetData_No()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        CertLossDataPanel.SetActive(false);
    }

    public void Credit()
    {
        SoundManager.instance.PlaySound("ButtonSound");

        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        SoundManager.instance.PlaySound("ButtonSound");

        CertQuitPanel.SetActive(true);
    }

    public void QuitGame_Yes()
    {
        SoundManager.instance.PlaySound("ButtonSound");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void QuitGame_No()
    {
        SoundManager.instance.PlaySound("ButtonSound");

        CertQuitPanel.SetActive(false);
    }

}
