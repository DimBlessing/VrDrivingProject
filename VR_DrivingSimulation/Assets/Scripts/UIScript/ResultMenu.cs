using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace SlimUI.ModernMenu
{
    public class ResultMenu : MonoBehaviour
    {
        Animator CameraObject;
        public GameObject ResultCanv;
        public GameObject Score_Manager;
        public TMP_Text ScoreText;
        private int end_trigger;//특정 조건을 완료할 경우 해당 값을 변화시킴.
        public int end_status;//외부에서 게임 종료 신호를 받아들였음을 확인하기 위한 변수
        private bool is_loading;
        [Header("Loaded Scene")]
        [Tooltip("The name of the scene in the build settings that will load")]

        [Header("LOADING SCREEN")]
        public GameObject loadingMenu;
        public Slider loadBar;
        public TMP_Text finishedLoadingText;
        public bool requireInputForNextScene = false;

        public enum Theme { custom1, custom2, custom3 };
        [Header("Theme Settings")]
        public Theme theme;
        int themeIndex;
        public FlexibleUIData themeController;

        private int nowLevel;
        private int is_Playing = 1;

        // Start is called before the first frame update
        void Start()
        {
            is_Playing = 1;
            is_loading = false;
            end_trigger = 0;
            end_status = end_trigger;
            nowLevel = SceneManager.GetActiveScene().buildIndex;
            CameraObject = transform.GetComponent<Animator>();
            ResultCanv.SetActive(false);
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

        // Update is called once per frame

        public void OpenResultCanv()
        {
            Time.timeScale = 0;
            ResultCanv.SetActive(true);
        }

        public void setEndTrriger(int ending_number)
        {
            end_trigger = ending_number;
            end_status = end_trigger;
        }

        public int getEndTrriger()
        {
            return end_trigger;
        }

        public bool GetLoadingStat()
        {
            return is_loading;
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
            GameSetting.Instance.Level = 0;
            is_Playing = 0;
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