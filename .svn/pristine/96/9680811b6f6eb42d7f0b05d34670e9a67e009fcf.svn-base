using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : Singleton<CharacterSelectionManager> {

    public List<GameObject> characters;
    [HideInInspector] public List<bool> isLocked = new List<bool>();
    public int currentCharacterIndex { get; private set; }
    public List<int> unlockByForceHistory = new List<int>();

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentCharacterIndex = 0;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /* main menu, credit, characterSelectionMenu, or LevelSelectionMenu */
        if (scene.buildIndex == 0 || scene.buildIndex == 13 || scene.buildIndex == 14 || scene.buildIndex == 15)
            return;

        GameObject currentCharObj = Instantiate(characters[currentCharacterIndex]);
        Character currentCharacter = currentCharObj.AddComponent<Character>();
        CopyInformation(Characters.instance.oldCharacter, currentCharacter);
        Characters.instance.AddCharacter(currentCharacter);
        Destroy(Characters.instance.oldCharacter.gameObject);
        currentCharacter.transform.parent = Characters.instance.transform;
    }

    // called once
    private void Start()
    {
        ResetData();
        LoadCharacter();
    }

    public void ResetData()
    {
        isLocked.Clear();

        for (int i = 0; i < 15; ++i)
            isLocked.Add(true);

        isLocked[0] = false; // Humburger is unlocked
        isLocked[1] = false; // RedDragon is unlocked
    }

    void CopyInformation(Character source, Character destination)
    {
        destination.transform.position = source.transform.position;
        //destination.transform.rotation = source.transform.rotation;
        destination.transform.localScale = source.transform.localScale;
        destination.speed = source.speed;
        destination.isCaptured = source.isCaptured;
        destination.isDivisible = source.isDivisible;
        //destination.rb = destination.GetComponent<Rigidbody>();
        //destination.characterCollision = destination.GetComponent<Collider>();
    }

    public void OnCharacterSelect(int index)
    {
        currentCharacterIndex = index;
    }

    public void UnlockCharacter(int index)
    {
        if (!isLocked[index])
            return;

        GameSceneManager.instance.isCharUnlockedInThisStage[index] = true;
    }

    public void UnlockCharacterByForce(int index)
    {
        if (!isLocked[index])
            return;

        unlockByForceHistory.Add(index);
    }

    public void SaveCharacter()
    {
        PlayerData saver = SaveLoadManager.instance.dataSavedByOtherClasses;
        saver.isCharacterSaved = true;

        CharacterSaver characterSelectionManagerCopy = new CharacterSaver();

        for (int i = 0; i < isLocked.Count; ++i)
            characterSelectionManagerCopy.isLocked.Add(isLocked[i]);

        saver.characterSaver = characterSelectionManagerCopy;

        SaveLoadManager.instance.Save();
    }

    void LoadCharacter()
    {
        SaveLoadManager.instance.Load();

        PlayerData loader = SaveLoadManager.instance.dataSavedByOtherClasses;

        if (loader.isCharacterSaved)
        {
            for (int i = 0; i < loader.characterSaver.isLocked.Count; ++i)
                isLocked[i] = loader.characterSaver.isLocked[i];
        }
    }
}

[System.Serializable]
public class CharacterSaver
{
    public List<bool> isLocked = new List<bool>();
}