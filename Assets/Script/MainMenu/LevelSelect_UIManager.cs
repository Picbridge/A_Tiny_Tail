using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect_UIManager : MonoBehaviour
{
    private Vector2 UIWIDTH = new Vector2(2014,0);
    const int TOTALLEVEL = 12;
    const int LEVELPERSCENE = 3;
    enum MENUSTATES {LEVEL1,LEVEL2,LEVEL3,LEVEL4,LEVELSET };
    MENUSTATES currentState;

    public GameObject level;
    public GameObject levelToLevelSet;
    public GameObject levelSet;
    public GameObject left;
    public GameObject right;

    private GameObject lava;
    private GameObject field;
    private GameObject horror;
    private GameObject ice;
    private GameObject main;

    float speed = 0.1f;
    float arrowAppear = 50;
    bool isInGameSelect = false;
    bool isRightClick;

    Vector3 visiblePosition = new Vector3(-1.5f, 10.6f, -6.8f);
    Vector3 invisiblePosition = new Vector3(-1.5f, 10.6f, -6f);

    private void Awake()
    {
        lava   = FindObjectOfType<LavaScene>().gameObject;  
        field  = FindObjectOfType<FieldScene>().gameObject; 
        horror = FindObjectOfType<HorrorScene>().gameObject;
        ice    = FindObjectOfType<IceScene>().gameObject;   
        main   = FindObjectOfType<MainScene>().gameObject;  

        lava.transform.position   = invisiblePosition;
        field.transform.position  = invisiblePosition;
        horror.transform.position = invisiblePosition;
        ice.transform.position    = invisiblePosition;
        main.transform.position   = visiblePosition;

        RankingManager ranking = RankingManager.instance;
        currentState = MENUSTATES.LEVELSET;

        // reset
        for (int i = 0; i < TOTALLEVEL-1; ++i)
            level.transform.GetChild((int)((i + 1) / LEVELPERSCENE) + TOTALLEVEL).GetChild((i + 1) % LEVELPERSCENE).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        for (int i = 0; i < ranking.latestClearLevel; i++)
        {
            if (ranking.levelRank[i] >= 1)
                level.transform.GetChild((int)(i / LEVELPERSCENE)).GetChild((i % LEVELPERSCENE)).GetChild(0).GetChild(0).gameObject.SetActive(true);

            if (ranking.levelRank[i] >= 2)
                level.transform.GetChild((int)(i / LEVELPERSCENE)).GetChild((i % LEVELPERSCENE)).GetChild(0).GetChild(1).gameObject.SetActive(true);

            if (ranking.levelRank[i] >= 3)
                level.transform.GetChild((int)(i / LEVELPERSCENE)).GetChild((i % LEVELPERSCENE)).GetChild(0).GetChild(2).gameObject.SetActive(true);

            if ((i + 1) > TOTALLEVEL - 1)
                break;

            level.transform.GetChild((int)((i + 1) / LEVELPERSCENE) + TOTALLEVEL).GetChild((i + 1) % LEVELPERSCENE).GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
            level.transform.GetChild((int)((i + 1) / LEVELPERSCENE)).GetChild((i + 1) % LEVELPERSCENE).GetComponent<Image>().raycastTarget = true;
        }
    }

    private void Start()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.ChooseBGM(0);
        SoundManager.instance.PlayBGM();
    }

    private void Update()
    {
        switch (currentState)
        {
            case MENUSTATES.LEVELSET:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        SceneManager.LoadScene("MainMenu");

                    level.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(false);
                    levelToLevelSet.SetActive(false);
                    levelSet.SetActive(true);
                    isInGameSelect = false;

                    lava.transform.position   = invisiblePosition;
                    field.transform.position  = invisiblePosition;
                    horror.transform.position = invisiblePosition;
                    ice.transform.position    = invisiblePosition;
                    main.transform.position   = visiblePosition;

                    break;
                }
            case MENUSTATES.LEVEL1:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        OnLevelSetUI();

                    level.SetActive(true);                
                    levelSet.SetActive(false);

                    lava.transform.position   = visiblePosition;
                    field.transform.position  = invisiblePosition;
                    horror.transform.position = invisiblePosition;
                    ice.transform.position    = invisiblePosition;
                    main.transform.position   = invisiblePosition;

                    if (!isInGameSelect)
                    {
                        left.SetActive(false);
                        right.SetActive(true);
                        levelToLevelSet.SetActive(true);
                        level.GetComponent<RectTransform>().anchoredPosition = UIWIDTH;
                    }
                    else
                    {
                        level.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(level.GetComponent<RectTransform>().anchoredPosition, UIWIDTH, speed);
                        if (level.GetComponent<RectTransform>().anchoredPosition.x >= UIWIDTH.x- arrowAppear)
                        {
                            left.SetActive(false);
                            right.SetActive(true);
                            levelToLevelSet.SetActive(true);
                        }
                    }
                    isInGameSelect = true;
                    break;
                }
            case MENUSTATES.LEVEL2:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        OnLevelSetUI();

                    level.SetActive(true);
                    levelSet.SetActive(false);

                    lava.transform.position   = invisiblePosition;
                    field.transform.position  = visiblePosition;
                    horror.transform.position = invisiblePosition;
                    ice.transform.position    = invisiblePosition;
                    main.transform.position   = invisiblePosition;

                    if (!isInGameSelect)
                    {
                        left.SetActive(true);
                        right.SetActive(true);
                        levelToLevelSet.SetActive(true);
                        level.GetComponent<RectTransform>().anchoredPosition = 0 * UIWIDTH;
                    }
                    else
                    {
                        level.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(level.GetComponent<RectTransform>().anchoredPosition, 0 * UIWIDTH, speed);
                        if (level.GetComponent<RectTransform>().anchoredPosition.x <= 0 * UIWIDTH.x+ arrowAppear && isRightClick)
                        {
                            left.SetActive(true);
                            right.SetActive(true);
                            levelToLevelSet.SetActive(true);
                        }
                        else if (level.GetComponent<RectTransform>().anchoredPosition.x >= 0 * UIWIDTH.x - arrowAppear && !isRightClick)
                        {
                            left.SetActive(true);
                            right.SetActive(true);
                            levelToLevelSet.SetActive(true);
                        }
                    }
                        isInGameSelect = true;
                    break;
                }
            case MENUSTATES.LEVEL3:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        OnLevelSetUI();

                    level.SetActive(true);
                    levelSet.SetActive(false);

                    lava.transform.position   = invisiblePosition;
                    field.transform.position  = invisiblePosition;
                    horror.transform.position = visiblePosition;
                    ice.transform.position    = invisiblePosition;
                    main.transform.position   = invisiblePosition;

                    if (!isInGameSelect)
                    {
                        left.SetActive(true);
                        right.SetActive(true);
                        levelToLevelSet.SetActive(true);
                        level.GetComponent<RectTransform>().anchoredPosition = -1 * UIWIDTH;
                    }
                    else
                    {
                        level.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(level.GetComponent<RectTransform>().anchoredPosition, -1 * UIWIDTH, speed);
                        if (level.GetComponent<RectTransform>().anchoredPosition.x <= -1 * UIWIDTH.x + arrowAppear && isRightClick)
                        {
                            left.SetActive(true);
                            right.SetActive(true);
                            levelToLevelSet.SetActive(true);
                        }
                        else if (level.GetComponent<RectTransform>().anchoredPosition.x >= -1 * UIWIDTH.x - arrowAppear && !isRightClick)
                        {
                            left.SetActive(true);
                            right.SetActive(true);
                            levelToLevelSet.SetActive(true);
                        }
                    }

                    isInGameSelect = true;
                    break;
                }
            case MENUSTATES.LEVEL4:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        OnLevelSetUI();

                    level.SetActive(true);
                    levelSet.SetActive(false);

                    lava.transform.position   = invisiblePosition;
                    field.transform.position  = invisiblePosition;
                    horror.transform.position = invisiblePosition;
                    ice.transform.position    = visiblePosition;
                    main.transform.position   = invisiblePosition;

                    if (!isInGameSelect)
                    {
                        left.SetActive(true);
                        right.SetActive(false);
                        levelToLevelSet.SetActive(true);
                        level.GetComponent<RectTransform>().anchoredPosition = -2*UIWIDTH;
                    }
                    else
                    {
                        level.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(level.GetComponent<RectTransform>().anchoredPosition, -2*UIWIDTH, speed);
                        if (level.GetComponent<RectTransform>().anchoredPosition.x <= -2*UIWIDTH.x + arrowAppear)
                        {
                            left.SetActive(true);
                            right.SetActive(false);
                            levelToLevelSet.SetActive(true);
                        }
                    }
                    isInGameSelect = true;
                    break;
                }
        }

    }

    public void OnLevel1UI()
    {
        currentState = MENUSTATES.LEVEL1;
    }

    public void OnLevel2UI()
    {
        currentState = MENUSTATES.LEVEL2;
    }

    public void OnLevel3UI()
    {
        currentState = MENUSTATES.LEVEL3;
    }

    public void OnLevel4UI()
    {
        currentState = MENUSTATES.LEVEL4;
    }

    public void OnLevelSetUI()
    {
        currentState = MENUSTATES.LEVELSET;
    }

    public void ClickLeft()
    {
        switch (currentState)
        {
            case MENUSTATES.LEVEL2:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = false;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL1;
                    break;
                }
            case MENUSTATES.LEVEL3:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = false;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL2;
                    break;
                }
            case MENUSTATES.LEVEL4:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = false;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL3;
                    break;
                }
        }

    }

    public void ClickRight()
    {
        switch (currentState)
        {
            case MENUSTATES.LEVEL1:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = true;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL2;
                    break;
                }
            case MENUSTATES.LEVEL2:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = true;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL3;    
                    break;
                }
            case MENUSTATES.LEVEL3:
                {
                    left.SetActive(false);
                    right.SetActive(false);
                    isRightClick = true;
                    levelToLevelSet.SetActive(false);
                    currentState = MENUSTATES.LEVEL4;  
                    break;
                }
        }
    }
    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickScene(string sceneName)
    {
        RankingManager.instance.SetLevelIndex(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
