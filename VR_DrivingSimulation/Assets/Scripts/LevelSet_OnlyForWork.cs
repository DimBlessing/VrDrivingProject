using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum level
{
    Level1, Level2, Level3
}
public class LevelSet_OnlyForWork : MonoBehaviour
{
    public level level_select;
    private int level_number;
    
    private void Awake()
    {
        switch (level_select)
        {
            case level.Level1:
                level_number = 1;
                break;
            case level.Level2:
                level_number = 2;
                break;
            case level.Level3:
                level_number = 3;
                break;
        }
        GameSetting.Instance.Level = level_number;
    }
}
