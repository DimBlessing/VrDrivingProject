using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavArrowCarType : MonoBehaviour
{
    public GameObject CarG29;
    public GameObject CarKeyBoard;
    public GameObject CarG29_NavArrow;
    public GameObject CarKeyBoard_NavArrow;

    private void Awake()
    {
        if (CarG29.activeSelf == true)
        {
            CarG29_NavArrow.SetActive(true);
            CarKeyBoard_NavArrow.SetActive(false);
        }
        else
        {
            CarG29_NavArrow.SetActive(false);
            CarKeyBoard_NavArrow.SetActive(true);
        }
    }
}
