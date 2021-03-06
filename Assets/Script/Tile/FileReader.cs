using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileReader : MonoBehaviour
{

    KeyValuePair<string, string> Key_Value;
    KeyValuePair<Vector2, string> Position_Coordinate;
    //L : left, U : up, R : right, D : down
    KeyValuePair<bool, bool[]> LURD;

    bool IsAlreadyTilemapSet = false;
  
    private void Start()
    {
       
    }

    public void ReadLevel(Level level)
    {
        if (level.levelName == "")
        {
            Debug.Log("THERE IS NO TEXT FILE!");
        }

        string[] lines;
        var list = new List<string>();        
        var fileStream = new FileStream(level.levelName, FileMode.Open, FileAccess.Read);

        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        lines = list.ToArray();
        
        for (int i = 0; i < lines.Length; i++)
        {
            //text file comments
            if (lines[i].Contains("//"))
            {
                //Debug.Log("COMMENTS: " + lines[i]);
                continue;
            }

            //Seperate Key and Value with given string
            Key_Value = GetKey_Value(lines[i]);

            //level, height, width section
            if (Key_Value.Key == "Level")
            {
                level.id = int.Parse(Key_Value.Value);
            }
            else if (Key_Value.Key == "Height")
            {
                level.map_height = int.Parse(Key_Value.Value);
            }
            else if (Key_Value.Key == "Width")
            {
                level.map_width = int.Parse(Key_Value.Value);
            }
            
            //if given string is coordinate information
            else if (Key_Value.Key.Contains(","))
            {
                if (IsAlreadyTilemapSet == false)
                {
                    IsAlreadyTilemapSet = true;
                    level.TileSetting();
                }                

                Position_Coordinate = GetCoordinate(lines[i]);
                
                LURD = SeperateLURD(Position_Coordinate.Value);                

                if (LURD.Key == true)
                {
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingLeft = false;
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingUp = false;
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingRight = false;
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingDown = false;

                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingLeft_A = LURD.Value[2];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingUp_A = LURD.Value[3];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingRight_A = LURD.Value[4];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingDown_A = LURD.Value[5];

                }
                else
                {                                                  
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingLeft = LURD.Value[0];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingUp = LURD.Value[1];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingRight = LURD.Value[2];
                    level.tile_map[(int)Position_Coordinate.Key.x, (int)Position_Coordinate.Key.y].isGoingDown = LURD.Value[3];
                }

            }
        }
        IsAlreadyTilemapSet = false;
    }

    KeyValuePair<string, string> GetKey_Value(string lines)
    {        
        char[] String = lines.ToCharArray();
        string Key = "";
        string Value = "";
        bool IsKey = true;
            
        for (int i = 0; i < String.Length; i++)
        {
            if (String[i] == ' ')
            {
                IsKey = false;
                continue;
            }

            if (IsKey == true)
                Key += String[i].ToString();
            else
                Value += String[i].ToString();            
        }

        return new KeyValuePair<string, string>(Key, Value);
    }

    KeyValuePair<Vector2, string> GetCoordinate(string lines)
    {
        char[] String = lines.ToCharArray();
        int cuttingPoint = 0;

        string Vector_x = "";
        string Vector_y = "";
        string Value = "";
        bool IsX = true;

        for (int i = 0; i < String.Length; i++)
        {
            if (String[i] == ',')
            {
                IsX = false;
                continue;
            }
            else if (String[i] == ' ')
            {
                cuttingPoint = i;
                break;
            }

            if (IsX == true)
                Vector_x += String[i].ToString();
            else
                Vector_y += String[i].ToString();
        }

        for (int i = cuttingPoint; i < String.Length; i++)
        {
            if(String[i] != ' ')
                Value += String[i].ToString();
        }

        return new KeyValuePair<Vector2, string>(new Vector2(int.Parse(Vector_x), int.Parse(Vector_y)), Value);
    }

    KeyValuePair<bool, bool[]> SeperateLURD(string LURD)
    {
        bool isContainingB = false;
        bool[] returnValue;

        if (LURD.Contains("B") || LURD.Contains("b"))
        {
            isContainingB = true;
            returnValue = new bool[6];
        }
        else
        {
            isContainingB = false;
            returnValue = new bool[4];
        }

        char[] String = LURD.ToCharArray();
     
        for (int i = 0; i < String.Length; i++)
        {
            if (String[i] == 'T' || String[i] == 't')
                returnValue[i] = true;

            else if (String[i] == 'F' || String[i] == 'f')
                returnValue[i] = false;

            else if (String[i] == 'B' || String[i] == 'b' || String[i] == '/')
                continue;

            else
            {
                returnValue[i] = false;
                //Debug.Log("TEXT FILE ERROR!! : only T,F,B are can be written in this section");
            }
        }
        return new KeyValuePair<bool, bool[]>(isContainingB, returnValue);
    }
}