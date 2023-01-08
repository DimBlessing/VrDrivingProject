using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : SingleTon<GameSetting>
{
    //private enum DPI { FHD, HDPlus, HD, }
    //private enum Controller { Keyboard, G29, }
    [SerializeField]
    private int currentDPI = 0;
    public int CurrentDPI { get { return currentDPI; } set { currentDPI = value; } }
    [SerializeField]
    private int currentController = 1;
    public int CurrentController { get { return currentController; } set { currentController = value; } }
    [SerializeField]
    private int level = 0;
    public int Level { get { return level; } set { level = value; } }
}
