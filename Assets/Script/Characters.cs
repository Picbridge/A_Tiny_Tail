using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {

    #region Singleton

    public static Characters instance; // variable to store the actual instance for the class object

    void Awake()
    {
        /* if object is already created */
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Characters found!");
            return;
        }
        instance = this;
    }

    #endregion

    public Character oldCharacter;
    private List<Character> characters = new List<Character>();
    private List<Character> removedCharacters = new List<Character>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsAvailableCharacterExist() && !GameSceneManager.instance.pausePanel.activeInHierarchy)
            GameSceneManager.instance.restartRecommendPanel.SetActive(true);
	}

    public void AddCharacter(Character character)
    {
        character.name = "clone " + characters.Count.ToString();

        for (int i = 0; i < characters.Count; ++i)
        {
            if (characters[i].name == character.name)
            {
                Debug.LogWarning("Duplicated character is on the list : " + character);
                return;
            }
        }        

        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (characters[i].name == character.name)
            {
                characters.Remove(character);
                character.name = "clone - removed";
                SortCharacterName();
                removedCharacters.Add(character);
                return;
            }
        }

        Debug.LogWarning("Character not on the list is attempted to delete : " + character);
    }

    void SortCharacterName()
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            characters[i].name = "clone " + i.ToString();
        }
    }

    bool IsAvailableCharacterExist()
    {
        if (characters.Count == 0)
            return false;

        for (int i = 0; i < characters.Count; ++i)
        {
            if (characters[i].isCaptured == false)
                return true;
        }
        return false;
    }

    public int GetAvailableCharactersNum()
    {
        return characters.Count;
    }

    public int GetRemovedCharactersNum()
    {
        return removedCharacters.Count;
    }
}
