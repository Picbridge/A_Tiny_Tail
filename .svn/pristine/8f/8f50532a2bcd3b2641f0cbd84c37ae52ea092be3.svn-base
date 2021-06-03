using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour {

    private List<GameObject> characters = new List<GameObject>();
    private GameObject currentCharacter;
    private int currentIndex;
    public Text numberText;
    public Text nameText;
    public GameObject locker;
    public ParticleSystem ps;
    public GameObject explanationPanel;
    public Text explanationText;
    private List<string> explanations = new List<string>();

    #region Singleton

    public static CharacterManager instance; // variable to store the actual instance for the class object

    void Awake()
    {
        /* if object is already created */
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CharacterManager found!");
            return;
        }
        instance = this;
    }

    #endregion
    
    // Use this for initialization
    void Start () {

        for (int i = 0; i < transform.childCount; ++i)
            characters.Add(transform.GetChild(i).gameObject);

        currentIndex = CharacterSelectionManager.instance.currentCharacterIndex;
        currentCharacter = characters[currentIndex];

        for (int i = 0; i < characters.Count; ++i)
            characters[i].SetActive(false);

        characters[currentIndex].SetActive(true);

        numberText.text = (currentIndex + 1).ToString() + "/" + characters.Count.ToString();
        nameText.text = currentCharacter.name.ToString();

        explanations.Add("Our team Mascot character");
        explanations.Add("DigiPen Mascot character");
        explanations.Add("Unlock by passing over the hollow hole without falling");
        explanations.Add("Unlock by clearing a stage over 2 minutes");
        explanations.Add("Unlock by clearing Horror Theme 2nd stage with minimum use of lever");
        explanations.Add("Unlock by clearing Ice Theme 2nd stage");
        explanations.Add("Unlock by clearing Ice Theme 3rd stage");
        explanations.Add("Unlock by using the same cannon twice");
        explanations.Add("Unlock by clearing Horror Theme 1st stage");
        explanations.Add("Unlock by clearing Horror Theme 3rd stage");
        explanations.Add("Unlock by clearing Field Theme 2nd stage");
        explanations.Add("Unlock by clearing Field Theme 3rd stage");
        explanations.Add("Unlock when the character who ate the candy fell into a hole and clear that stage");
        explanations.Add("Unlock by restarting the stage and clear that stage");
        explanations.Add("Unlock when DigiPen dragon ate the candy and finished any stage");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightArrowClicked();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftArrowClicked();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSelect();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressBack();
        }
	}

    void ChangeCharacterSkin()
    {
        if (currentIndex < 0 || currentIndex >= characters.Count)
        {
            Debug.LogWarning("ChangeCharacterSkin() out of index");
            return;
        }

        //if (characters[index].locked)
        //{
        //    Debug.LogWarning("ChangeCharacterSkin() invalid index access");
        //    return;
        //}

        currentCharacter.gameObject.SetActive(false);
        currentCharacter = characters[currentIndex]; // 캐릭터가 가진 정보까지 바꿔야하나? // 씬 중간에 스킨을 바꿀 경우 정보 저장까지
        currentCharacter.gameObject.SetActive(true);
        numberText.text = (currentIndex + 1).ToString() + "/" + characters.Count.ToString();
        nameText.text = currentCharacter.name.ToString();
    }

    public void OnLeftArrowClicked()
    {
        --currentIndex;
        if (currentIndex < 0)
            currentIndex = characters.Count - 1;

        ChangeCharacterSkin();
        UpdateLocker();
        explanationText.text = explanations[currentIndex];
    }

    public void OnRightArrowClicked()
    {
        ++currentIndex;
        if (currentIndex >= characters.Count)
            currentIndex = 0;

        ChangeCharacterSkin();
        UpdateLocker();
        explanationText.text = explanations[currentIndex];
    }

    public void OnSelect()
    {
        if (!CharacterSelectionManager.instance.isLocked[currentIndex])
        {
            CharacterSelectionManager.instance.OnCharacterSelect(currentIndex);
            ps.Play();
        }
    }

    void UpdateLocker()
    {
        locker.SetActive(CharacterSelectionManager.instance.isLocked[currentIndex]);
    }

    public void OnPressBack()
    {
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void OnPressQuestionMark()
    {
        explanationPanel.SetActive(!explanationPanel.activeSelf);
        explanationText.text = explanations[currentIndex];
    }
}
