using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level
{
    public int id;
    public int map_height;
    public int map_width;
    public string levelName;

    public Tile[,] tile_map;

    public Level(int id_)
    {
        id = id_;
        map_height = 0;
        map_width = 0;
    }

    public void TileSetting()
    {
        //Map height and width are not real height and width of level!!
        //txt file's height = real height - 1 
        //txt file's width = real width - 1 
        if (map_height == 0 || map_width == 0)
        {
            Debug.Log("HEIGHT OR WIDTH OF MAP IS NOT SET!");
        }
        else
        {
            tile_map = new Tile[map_height + 1, map_width + 1];

            for (int i = 0; i < map_height + 1; i++)
            {
                for (int j = 0; j < map_width + 1; j++)
                {
                    tile_map[i, j] = new Tile();
                }
            }
        }
    }
}

public class Tile
{
    public bool isGoingLeft;
    public bool isGoingUp;
    public bool isGoingRight;
    public bool isGoingDown;

    public bool isGoingLeft_A;
    public bool isGoingUp_A;
    public bool isGoingRight_A;
    public bool isGoingDown_A;
}

public class TileMapManager : Singleton<TileMapManager>
{
    public FileReader fileReader;
    public int levelSize;

    public Level[] level;
    public float tilePortion;

    void Start()
    {
        level = new Level[levelSize];

        for (int i = 0; i < levelSize; i++)
            level[i] = new Level(i);

        level[0].levelName  = "H_Tutorial.txt";
        level[1].levelName  = "H_Lv1.txt";
        level[2].levelName  = "H_Lv2.txt";
        level[3].levelName  = "C_Tutorial.txt";
        level[4].levelName  = "C_Lv1.txt";
        level[5].levelName  = "C_Lv2.txt";
        level[6].levelName  = "L_Tutorial.txt";
        level[7].levelName  = "L_Lv1.txt";
        level[8].levelName  = "L_Lv2.txt";
        level[9].levelName  = "M_Lv1.txt";
        level[10].levelName = "M_Lv2.txt";
        level[11].levelName = "M_Lv3.txt";


        tilePortion = 1.0f;

        for (int i = 0; i < levelSize; i++)
        {
            fileReader.ReadLevel(level[i]);
        }
    }

    void swap(ref bool a, ref bool b)
    {
        bool temp = a;
        a = b;
        b = temp;
    }

    public void UpdateAdjacentTile(Vector2 tilePos)
    {
        Level tempLevel = level[GameSceneManager.instance.currentLevelIndex];

        //Right check
        if (tempLevel.map_width > tilePos.y + 1)
        {
            tempLevel.tile_map[(int)tilePos.x, (int)tilePos.y + 1].isGoingLeft = level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingRight;
        }

        //Left Check
        if (tilePos.y - 1 > 0)
        {
            tempLevel.tile_map[(int)tilePos.x, (int)tilePos.y - 1].isGoingRight = level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingLeft;
        }

        //Up Check
        if (tilePos.x - 1 > 0)
        {
            tempLevel.tile_map[(int)tilePos.x - 1, (int)tilePos.y].isGoingUp = level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingDown;
        }

        //Down Check
        if (tempLevel.map_height < tilePos.x + 1)
        {
            tempLevel.tile_map[(int)tilePos.x + 1, (int)tilePos.y].isGoingDown = level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingUp;
        }
    }

    public void UpdateTileMap(Vector2 tilePos)
    {
        //Level tempLevel = level[GameSceneManager.instance.currentLevelIndex];

        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingLeft);
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingUp);
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingRight);                
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingDown);

        swap(ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingRight, ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingRight_A);
        swap(ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingLeft, ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingLeft_A);
        swap(ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingDown, ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingDown_A);
        swap(ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingUp, ref level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingUp_A);

        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingLeft);
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingUp);
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingRight);
        Debug.Log(level[GameSceneManager.instance.currentLevelIndex].tile_map[(int)tilePos.x, (int)tilePos.y].isGoingDown);

        UpdateAdjacentTile(tilePos);
    }
}
