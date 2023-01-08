using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectChange : MonoBehaviour
{

    private GameObject NButton;
    private GameObject PButton;
    public GameObject FirstButton;

    void Start()
    {
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


    public void SetNextButton(GameObject NextButton)
    {
        if (NextButton.activeSelf == true) NButton = NextButton;
        else NButton = FirstButton;
    }

    public void SetPrevButton(GameObject PrevButton)
    {
        if (PrevButton.activeSelf == true) PButton = PrevButton;
        else PButton = FirstButton;
    }

}
