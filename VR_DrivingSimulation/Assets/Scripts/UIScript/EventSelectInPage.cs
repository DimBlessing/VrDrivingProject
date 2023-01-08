using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSelectInPage : MonoBehaviour
{
    public GameObject ThisPage, NextPage, PrevPage;
    public GameObject PrevPageBtn;
    public GameObject EventList;
    private GameObject NButton;
    private GameObject PButton;
    public GameObject FirstButton;
    public GameObject BckButton;
    public GameObject MainMenuEventListBtn;

    void Start()
    {
        if (FirstButton.activeSelf == false) FirstButton = BckButton;
        EventSystem.current.SetSelectedGameObject(FirstButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventSystem.current.SetSelectedGameObject(PButton);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventSystem.current.SetSelectedGameObject(NButton);
        }
    }

    public void OpenThisPage()
    {
        ThisPage.SetActive(true);
        setFirstSelection();
    }
    public void setFirstSelection()
    {
        if (FirstButton.activeSelf == false) FirstButton = BckButton;
        EventSystem.current.SetSelectedGameObject(FirstButton);
    }
    public void CloseEventPage()
    {
        EventList.SetActive(false);
        EventSystem.current.SetSelectedGameObject(MainMenuEventListBtn);
    }
    public void SetNextButton(GameObject NextButton)
    {
        if (NextButton.activeSelf == true) NButton = NextButton;
        else NButton = PrevPageBtn;
    }

    public void SetPrevButton(GameObject PrevButton)
    {
        if (PrevButton.activeSelf == true) PButton = PrevButton;
        else PButton = FirstButton;
    }
    public void OpenNextPage(GameObject NextPageFirstBtn)
    {
        if (NextPageFirstBtn.activeSelf == false) return;
        else
        {
            ThisPage.SetActive(false);
            NextPage.SetActive(true);
            EventSystem.current.SetSelectedGameObject(NextPageFirstBtn);
        }
    }

    public void OpenPrevPage()
    {
        ThisPage.SetActive(false);
        PrevPage.SetActive(true);
    }
}
