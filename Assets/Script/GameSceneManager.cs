using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    #region Singleton

    public static GameSceneManager instance; // variable to store the actual instance for the class object

    void Awake()
    {
        /* if object is already created */
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameSceneManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public int currentLevelIndex;

    public GameObject timer;
    public Text time_Min;
    public Text time_Sec;

    public GameObject pausePanel;
    public GameObject optionPanel;    public GameObject winningPanel;
    public GameObject CertQuitPanel;
    public GameObject charUnlockPanel;
    public GameObject lastLevelPanel;
    public GameObject restartRecommendPanel;
    public List<Sprite> character2D;
    public Image charUnlockPanelImage;
    private int charUnlockImageIndex = -1;
    public GameObject[] star;
    public GameObject[] star_last;
    [HideInInspector] public List<bool> isCharUnlockedInThisStage = new List<bool>();

    AsyncOperation async;

    private void Start()
    {       
        RankingManager.instance.GameStart();
        StarInitialize();

        for (int i = 0; i < 15; ++i)
            isCharUnlockedInThisStage.Add(false);

        SoundManager.instance.StopBGM();

        if(RankingManager.instance.enterLevelIndex == 1 || RankingManager.instance.enterLevelIndex == 2 || RankingManager.instance.enterLevelIndex == 3)
            SoundManager.instance.ChooseBGM(0);
        if (RankingManager.instance.enterLevelIndex == 4 || RankingManager.instance.enterLevelIndex == 5 || RankingManager.instance.enterLevelIndex == 6)
            SoundManager.instance.ChooseBGM(0);
        if (RankingManager.instance.enterLevelIndex == 7 || RankingManager.instance.enterLevelIndex == 8 || RankingManager.instance.enterLevelIndex == 9)
            SoundManager.instance.ChooseBGM(0);
        if (RankingManager.instance.enterLevelIndex == 10 || RankingManager.instance.enterLevelIndex == 11 || RankingManager.instance.enterLevelIndex == 12)
            SoundManager.instance.ChooseBGM(0);

        SoundManager.instance.PlayBGM();
    }

    void Update ()
    {
        time_Min.text = ((int)(RankingManager.instance.timer / 60)).ToString();
        time_Sec.text = ((int)(RankingManager.instance.timer % 60)).ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
            PopPausePanel();

        UpdateCheatCode();
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            PopPausePanel();
        }
    }

    void UpdateCheatCode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MainMenu");            
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("H_Tutorial");
            RankingManager.instance.SetLevelIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("H_Lv1");
            RankingManager.instance.SetLevelIndex(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("H_Lv2");
            RankingManager.instance.SetLevelIndex(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("C_Tutorial");
            RankingManager.instance.SetLevelIndex(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("C_Lv1");
            RankingManager.instance.SetLevelIndex(5);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("C_Lv2");
            RankingManager.instance.SetLevelIndex(6);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("L_Tutorial");
            RankingManager.instance.SetLevelIndex(7);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("L_Lv1");
            RankingManager.instance.SetLevelIndex(8);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("L_Lv2");
            RankingManager.instance.SetLevelIndex(9);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("M_Lv1");
            RankingManager.instance.SetLevelIndex(10);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("M_Lv2");
            RankingManager.instance.SetLevelIndex(11);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("M_Lv3");
            RankingManager.instance.SetLevelIndex(12);
        }
    }

    public void PopPausePanel()
    {        
        bool active = !pausePanel.activeInHierarchy;
        bool active_win = !winningPanel.activeInHierarchy;

        if (active_win == false)
            return;

        if (active == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        pausePanel.SetActive(active);
        optionPanel.SetActive(false);
        restartRecommendPanel.SetActive(false);
    }

    public void PopupOptionPanel()
    {
        bool active = !optionPanel.activeInHierarchy;

        SoundManager.instance.PlaySound("ButtonSound");
        optionPanel.SetActive(active);        
    }

    public void PopUpWinningPanel()
    {
        CharacterSelectionManager unlockMgr = CharacterSelectionManager.instance;

        while (unlockMgr.unlockByForceHistory.Count != 0)
        {
            isCharUnlockedInThisStage[unlockMgr.unlockByForceHistory[0]] = true;
            unlockMgr.unlockByForceHistory.RemoveAt(0);
        }

        for (int i = 0; i < isCharUnlockedInThisStage.Count; ++i)
        {
            if (isCharUnlockedInThisStage[i])
            {
                PopUpCharacterUnlockPanel(i);
                break;
            }
        }

        winningPanel.SetActive(true);
        SoundManager.instance.PlaySound("ButtonSound");
    }

    public void PopLastLevelPanel()
    {
        CharacterSelectionManager unlockMgr = CharacterSelectionManager.instance;

        while (unlockMgr.unlockByForceHistory.Count != 0)
        {
            isCharUnlockedInThisStage[unlockMgr.unlockByForceHistory[0]] = true;
            unlockMgr.unlockByForceHistory.RemoveAt(0);
        }

        for (int i = 0; i < isCharUnlockedInThisStage.Count; ++i)
        {
            if (isCharUnlockedInThisStage[i])
            {
                PopUpCharacterUnlockPanel(i);
                break;
            }
        }

        lastLevelPanel.SetActive(true);
        SoundManager.instance.PlaySound("ButtonSound");
    }

    public void OnConfirmWin()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        SceneManager.LoadScene("LevelSelectionMenu");        
    }

    public void GoToNextLevel()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        RankingManager.instance.SetLevelIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToCharacterSelect()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        SceneManager.LoadScene("CharacterSelectionMenu");
    }

    public void ResumeGame()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        PopPausePanel();
    }

    public void RestartGame()
    {
        CharacterSelectionManager.instance.UnlockCharacterByForce(13); // Unlock oldtv
        Time.timeScale = 1;
        SoundManager.instance.PlaySound("ButtonSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GiveUpGame()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlaySound("ButtonSound");
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void Option()
    {
        SoundManager.instance.PlaySound("ButtonSound");
        PopupOptionPanel();
    }

    public void QuitGame()
    {
        SoundManager.instance.PlaySound("ButtonSound");

        CertQuitPanel.SetActive(true);
    }

    public void CertQuit_Yes()
    {
        SoundManager.instance.PlaySound("ButtonSound");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CertQuit_No()
    {
        SoundManager.instance.PlaySound("ButtonSound");

        CertQuitPanel.SetActive(false);
    }

    public void GoToCreditScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CreditScene");
    }

    public bool IsGoalPosOccupied(Vector3 goalPos)
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        int charNum = characters.Length;

        Vector3[] RoundedCharPos = new Vector3[charNum];

        for (int i = 0; i < charNum; i++)
        {
            RoundedCharPos[i] = new Vector3((float)Math.Round(characters[i].transform.position.x), (float)Math.Round(characters[i].transform.position.y), (float)Math.Round(characters[i].transform.position.z));
        }

        for (int i = 0; i < charNum; i++)
        {
            if (goalPos == RoundedCharPos[i])
                return false;
        }

        return true;
    }

    public void TimerOnOff()
    {
        bool active = timer.activeInHierarchy;

        timer.SetActive(!active);
    }

    public void StarRating(int num, int level)
    {
        for (int i = 0; i < num; i++)
            star[i].SetActive(true);

        if (level != 12)
        {
            for (int i = 0; i < num; i++)
                star[i].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 3; i++)
                star_last[i].SetActive(false);

            for (int i = 0; i < num; i++)
                star_last[i].SetActive(true);
        }
    }

    public void StarInitialize()
    {
        for (int i = 0; i < 3; i++)
            star[i].SetActive(false);
    }

    public void PopUpCharacterUnlockPanel(int index)
    {
        charUnlockPanel.SetActive(true);
        CharacterSelectionManager.instance.isLocked[index] = false;
        charUnlockPanelImage.sprite = character2D[index];
        charUnlockImageIndex = index;
    }

    public void OnConfirmCharacterUnlockPanel()
    {
        bool isThereAnyCharUnlockOnStack = false;

        for (int i = charUnlockImageIndex + 1; i < isCharUnlockedInThisStage.Count; ++i)
        {
            if (isCharUnlockedInThisStage[i])
            {
                isThereAnyCharUnlockOnStack = true;
                PopUpCharacterUnlockPanel(i);
                break;
            }
        }

        if (!isThereAnyCharUnlockOnStack)
        {
            charUnlockPanel.SetActive(false);
            CharacterSelectionManager.instance.SaveCharacter();
        }
    }

    public void OnConfirmCharacterChange()
    {
        CharacterSelectionManager.instance.OnCharacterSelect(charUnlockImageIndex);
        OnConfirmCharacterUnlockPanel();
    }
}

