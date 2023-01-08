using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventBtnActive : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[30];
    public GameObject[] button_page = new GameObject[5];
    public Button back_btn;
    public GameObject event_feed;
    public GameObject event_list;
    public Button feedback_back_btn;
    public int next_active = 0;


    private void Start()
    {
        next_active = 0;
    }



    public void event_btn_active()
    {
        if(next_active < 30) {
            buttons[next_active].SetActive(true);
            next_active += 1;
        }
    }



    public void OpenEventFeed(GameObject page)
    {
        page.SetActive(false);
        event_list.SetActive(false);
        event_feed.SetActive(true);
        feedback_back_btn.Select();
    }
    public void CloseEventList(GameObject event_list_page)
    {
        event_list_page.SetActive(false);
        back_btn.gameObject.SetActive(true);
    }

    public void btnSelect(Button page_back_btn)
    {
        if (buttons[0].GetComponent<Button>().IsActive() == false) page_back_btn.Select();
        else buttons[0].GetComponent<Button>().Select();
    }
}
