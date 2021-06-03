using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : Singleton<RankingManager>
{
    private bool isObjectObtained;
    private bool gameStarted;
    private bool snailChecker;

    public int[] levelRank;
    public float[] levelTimeStandard;
    public float timer;
    private float snail_timer;

    public int latestClearLevel = 0;
    public int enterLevelIndex;

	void Start ()
    {
        levelRank = new int[12];
        levelTimeStandard = new float[12];

        ResetData();
        LoadStar();
    }

    public void ResetData()
    {
        for (int i = 0; i < levelRank.Length; i++)
            levelRank[i] = 0;

        levelTimeStandard[0] = 600f;
        levelTimeStandard[1] = 30f;
        levelTimeStandard[2] = 45f;
        levelTimeStandard[3] = 600f;
        levelTimeStandard[4] = 50f;
        levelTimeStandard[5] = 49f;
        levelTimeStandard[6] = 600f;
        levelTimeStandard[7] = 55f;
        levelTimeStandard[8] = 57f;
        levelTimeStandard[9] = 40f;
        levelTimeStandard[10] = 70f;
        levelTimeStandard[11] = 100f;

        latestClearLevel = 0;
    }

    void Update()
    {        
        if (gameStarted == true)
        {            
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timer = 0;
            gameStarted = false;
        }

        if (snailChecker == true)
        {
            snail_timer += Time.deltaTime;
        }

    }

    public void GameStart()
    {            
        isObjectObtained = false;
        timer = levelTimeStandard[enterLevelIndex - 1];
        snail_timer = 0;
        gameStarted = true;
        snailChecker = true;
    }

    public int GameEnd(int levelIndex)
    {
        int rank = 1;
        gameStarted = false;
        snailChecker = false;

        if (latestClearLevel < levelIndex)
            latestClearLevel = levelIndex;

        if (timer > 0)        
            rank++;

        if (snail_timer > 120f)
            CharacterSelectionManager.instance.UnlockCharacter(3); // Unlock Snail

        if (isObjectObtained == true)
            rank++;

        if(levelRank[levelIndex - 1] < rank)
             levelRank[levelIndex - 1] = rank;

        SaveStar();

        return rank;
    }

    public void ObtainObject()
    {
        isObjectObtained = true;
    }

    public void SetLevelIndex(int index)
    {
        enterLevelIndex = index;
    }

    public void SetLevelIndex(string name)
    {
        if (name == "H_Tutorial")
            enterLevelIndex = 1;
        else if (name == "H_Lv1")
            enterLevelIndex = 2;
        else if (name == "H_Lv2")
            enterLevelIndex = 3;
        else if (name == "C_Tutorial")
            enterLevelIndex = 4;
        else if (name == "C_Lv1")
            enterLevelIndex = 5;
        else if (name == "C_Lv2")
            enterLevelIndex = 6;
        else if (name == "L_Tutorial")
            enterLevelIndex = 7;
        else if (name == "L_Lv1")
            enterLevelIndex = 8;
        else if (name == "L_Lv2")
            enterLevelIndex = 9;
        else if (name == "M_Lv1")
            enterLevelIndex = 10;
        else if (name == "M_Lv2")
            enterLevelIndex = 11;
        else if (name == "M_Lv3")
            enterLevelIndex = 12;
    }

    void SaveStar()
    {
        PlayerData saver = SaveLoadManager.instance.dataSavedByOtherClasses;
        saver.isStarSaved = true;

        RankSaver rankingManagerCopy = new RankSaver();

        for (int i = 0; i < levelRank.Length; ++i)
            rankingManagerCopy.levelRank[i] = levelRank[i];
        rankingManagerCopy.lastClearLevel = latestClearLevel;

        saver.starSaver = rankingManagerCopy;

        SaveLoadManager.instance.Save();
    }

    void LoadStar()
    {
        SaveLoadManager.instance.Load();

        PlayerData loader = SaveLoadManager.instance.dataSavedByOtherClasses;

        if (loader.isStarSaved)
        {
            for (int i = 0; i < loader.starSaver.levelRank.Length; ++i)
                levelRank[i] = loader.starSaver.levelRank[i];
            latestClearLevel = loader.starSaver.lastClearLevel;
        }
    }
}

[System.Serializable]
public class RankSaver
{
    public int[] levelRank = new int[12];
    public int lastClearLevel;
}