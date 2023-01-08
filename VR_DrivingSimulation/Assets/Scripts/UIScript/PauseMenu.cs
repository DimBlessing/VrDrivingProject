using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace SlimUI.ModernMenu
{
    public class PauseMenu : MonoBehaviour
    {
        Animator CameraObject;
        public GameObject PauseCanv;
        public GameObject MainPausePanel;
        public GameObject SelectLevelPanel;
        public GameObject AnswerPanel;
        public GameObject BackGroundPanel;
        private bool is_pause;

        [Header("Loaded Scene")]
        [Tooltip("The name of the scene in the build settings that will load")]

        [Header("LOADING SCREEN")]
        public GameObject loadingMenu;
        public Slider loadBar;
        public TMP_Text finishedLoadingText;
        public bool requireInputForNextScene = false;
        private bool is_loading = false;
        public enum Theme { custom1, custom2, custom3 };
        [Header("Theme Settings")]
        public Theme theme;
        int themeIndex;
        public FlexibleUIData themeController;

        private int nowLevel;
        private int is_Playing;

        // Start is called before the first frame update
        void Start()
        {
            is_loading = false;
            is_pause = false;
            is_Playing = 1;
            Time.timeScale = 1;
            nowLevel = SceneManager.GetActiveScene().buildIndex;
            CameraObject = transform.GetComponent<Animator>();
            PauseCanv.SetActive(false);
            BackGroundPanel.SetActive(false);
            MainPausePanel.SetActive(false);
            SelectLevelPanel.SetActive(false);
            AnswerPanel.SetActive(false);
            SetThemeColors();
        }

        void SetThemeColors()
        {
            if (theme == Theme.custom1)
            {
                themeController.currentColor = themeController.custom1.graphic1;
                themeController.textColor = themeController.custom1.text1;
                themeIndex = 0;
            }
            else if (theme == Theme.custom2)
            {
                themeController.currentColor = themeController.custom2.graphic2;
                themeController.textColor = themeController.custom2.text2;
                themeIndex = 1;
            }
            else if (theme == Theme.custom3)
            {
                themeController.currentColor = themeController.custom3.graphic3;
                themeController.textColor = themeController.custom3.text3;
                themeIndex = 2;
            }
        }


        public void Pause()
        {
            is_pause = true;
            Time.timeScale = 0;
            PauseCanv.SetActive(true);
            MainPausePanel.SetActive(true);
            BackGroundPanel.SetActive(true);
            SelectLevelPanel.SetActive(false);
            AnswerPanel.SetActive(false);
        }
        public void EndPause()
        {
            is_pause = false;
            Time.timeScale = 1;
            MainPausePanel.SetActive(false);
            SelectLevelPanel.SetActive(false);
            BackGroundPanel.SetActive(false);
            AnswerPanel.SetActive(false);
            PauseCanv.SetActive(false);
        }
        public bool GetPauseStatus()
        {
            return is_pause;
        }

        public bool GetLoadingStat()
        {
            return is_loading;
        }
        public void OpenSelectLevelPanel()
        {
            SelectLevelPanel.SetActive(true);
        }

        public void CloseSelectLevelPanel()
        {
            SelectLevelPanel.SetActive(false);
        }

        public void OpenAnswerPanel()
        {
            SelectLevelPanel.SetActive(false);
            MainPausePanel.SetActive(false);
            AnswerPanel.SetActive(true);
        }

        public void CloseAnswerPanel()
        {
            AnswerPanel.SetActive(false);
            MainPausePanel.SetActive(true);
        }

        public void LevelSelect(int selectingLevel)
        {
            GameSetting.Instance.Level = selectingLevel;
            startGame();
        }

        public void Restart()
        {
            GameSetting.Instance.Level = nowLevel;
            startGame();
        }
        public void GoMainMenu()
        {
            is_Playing = 0;
            GameSetting.Instance.Level = 0;
            startGame();
        }
        public void startGame()
        {
            StartCoroutine(LoadAsynchronously());
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }

        IEnumerator LoadAsynchronously()
        { // scene name is just the name of the current scene being loaded
            is_loading = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(is_Playing);
            operation.allowSceneActivation = false;
            loadingMenu.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadBar.value = progress;

                if (operation.progress >= 0.9f)
                {
                    if (requireInputForNextScene)
                    {
                        finishedLoadingText.gameObject.SetActive(true);

                        if (Input.anyKeyDown)
                        {
                            operation.allowSceneActivation = true;
                        }
                    }
                    else
                    {
                        operation.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
        }
    }
}