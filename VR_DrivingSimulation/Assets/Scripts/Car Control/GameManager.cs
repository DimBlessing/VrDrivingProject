using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class GameManager : SingleTon<GameManager>
{
    #region ���� �÷��� Ÿ�̸� ���� / Game Play Timer Variables
    private float timer = 0f; // Normal Timer
    private WaitForSeconds OneSecond = new WaitForSeconds(1.0f); // 1 sec
    private int threeTime = 3;
    private int min = 0;
    private int sec = 0;
    private int ms = 0;
    #endregion

    #region ���� ���� & ���� ���� / The Variables Game Setting And Play Control
    [SerializeField]
    private bool isGaming = true; // Use To Control Key Input Update
    public bool IsGaming { get { return isGaming; } }
    private bool isFullScreen = true; // Use to window setting
    private bool FirstLap = false; // Use to when finish lap
    public bool isFirstLap { get { return FirstLap; } set { FirstLap = value; } }
    private bool FinishLap = false; // Use to when finish lap
    public bool isFinishLap { get { return FinishLap; } set { FinishLap = value; } }
    // ===========ID Info ========== //

    [SerializeField]
    private CarController player = null; // Current Player Script
    private GameObject PlayerPrefab = null; // Selected Player Prefab
    private GameObject StartPosition = null; // Player Start Position
    #endregion

    public void InitAwake()
    {
        InitDPI(); // Window Size
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Car>();
        //SceneManager.activeSceneChanged -= WhenSceneChanged;
        //SceneManager.activeSceneChanged += WhenSceneChanged; // Scene Change Event Called Once
    } // Add Scene Change Event & Init Window
    private void Update()
    {
        /*
        if (isGaming) // Update Input Only In Game Scene
        {
            InputManager.Instance.OnUpdate(); // Input Update
        }
        */
    } // Never Use

    #region �� ���濡 ���� �÷��� ���� �ʱ�ȭ
    /*private void WhenSceneChanged(Scene previous, Scene now) // When Scene Changed, Called only once.
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Get current scene build index
        Debug.Log("SceneChanged! : " + CurrentSceneIndex);
        isGaming = false; // Init Values to NULL
        player = null; // Init Values to NULL
        switch (CurrentSceneIndex) // Loaded Scene Initialize (Buttons, UI, Selected Car Prefab)
        {
            case 0:
                {
                    ButtonManager.Instance.InitMainSceneButton(); // Button Initialize
                    Debug.Log("Main Scene Loaded");
                    break;
                }
            case 1: // Car Select Scene
                {
                    ButtonManager.Instance.InitCarSelectButton();  // Button Initialize
                    break;
                }
            case 2: // Straight Course Scene
                {
                    ButtonManager.Instance.InitGameSceneButton(); // Button Initialize
                    InstantiatePrefabAndPosition(); // Create Player and Set Position
                    InitStraightScene(); // Init Scene
                    break;
                }
            case 3: // Dirt Course Scene
                {
                    ButtonManager.Instance.InitGameSceneButton(); // Button Initialize
                    InstantiatePrefabAndPosition();
                    InitDirtScene();
                    break;
                }
            case 4: // Record Scene
                {
                    ButtonManager.Instance.InitRecordSceneButton(); // Button Initialize
                    record.ShowRecord();
                    break;
                }
            case 5: // Setting Scene
                {
                    ButtonManager.Instance.InitSettingSceneButton();
                    break;
                }
            default: { break; }
        }
    }*/
   
    #endregion

 
    private void InitDPI() // Set the window size
    {
        switch(GameSetting.Instance.CurrentDPI) // Get from Game Setting Class
        {
            case 0: // 1920 1080
                {
                    Screen.SetResolution(1920, 1080, isFullScreen); // FHD
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1600, 900, isFullScreen); // HD+
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1280, 720, isFullScreen); // HD
                    break;
                }
            default:
                {
                    Screen.SetResolution(1920, 1080, isFullScreen); // Default Value
                    break;
                }
        }
    }
}
