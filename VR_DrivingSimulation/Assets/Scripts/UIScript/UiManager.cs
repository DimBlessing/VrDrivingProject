using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimUI.ModernMenu
{
    public class UiManager : MonoBehaviour
    {
        private short game_status = 0;//0: 게임 진행 중, 1: 일시 정지, 10: 게임 종료(피드백)
        [Header("UICamera Object(has PauseMenu & Result Menu scripts)")]
        public GameObject UI_Camera;
        //[Header("ScoreBoardObject")]
        //public GameObject ScoreBoard;
        // Start is called before the first frame update
        void Start()
        {
            game_status = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (game_status != 0) AudioListener.pause = true;
            else AudioListener.pause = false;
            //if (game_status != 0) ScoreBoard.GetComponent<ScoreManager>().Now_Score_Canv.SetActive(false);
            //else ScoreBoard.GetComponent<ScoreManager>().Now_Score_Canv.SetActive(true);
            if (UI_Camera.GetComponent<ResultMenu>().end_status > 0)
            {
                game_status += 10;
                UI_Camera.GetComponent<ResultMenu>().end_status = -1;
            }
            if (UI_Camera.GetComponent<PauseMenu>().GetPauseStatus() == false && game_status % 10 == 1) game_status -= 1;
            if(game_status == 10) UI_Camera.GetComponent<ResultMenu>().OpenResultCanv();
            if(UI_Camera.GetComponent<PauseMenu>().GetLoadingStat() == true || UI_Camera.GetComponent<ResultMenu>().GetLoadingStat() == true)
            {
                UI_Camera.GetComponent<ResultMenu>().ResultCanv.SetActive(false);
                UI_Camera.GetComponent<PauseMenu>().EndPause();
                Time.timeScale = 0;
                game_status += 1000;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (game_status)
                {
                    case 0:
                        UI_Camera.GetComponent<PauseMenu>().Pause();
                        game_status = 1;
                        break;

                    case 1:
                        UI_Camera.GetComponent<PauseMenu>().EndPause();
                        game_status = 0;
                        break;

                    case 10:
                        UI_Camera.GetComponent<PauseMenu>().Pause();
                        UI_Camera.GetComponent<ResultMenu>().ResultCanv.SetActive(false);
                        game_status = 11;
                        break;

                    case 11:
                        UI_Camera.GetComponent<PauseMenu>().EndPause();
                        UI_Camera.GetComponent<ResultMenu>().OpenResultCanv();
                        game_status = 10;
                        break;
                }
            }
        }
    }
}