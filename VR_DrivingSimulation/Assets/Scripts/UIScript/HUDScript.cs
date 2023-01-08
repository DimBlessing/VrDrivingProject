using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDScript : MonoBehaviour
{
    public Rigidbody Car;
    public TMP_Text CurrentV;
    public TMP_Text Distance;
    [Header("SpeedLimitImage: 20 / 50 / 70")]
    public GameObject[] speedLimit = new GameObject[3];
    [Header("NaviArrow: Straight / Right / Left")]
    public GameObject[] NaviArrow = new GameObject[3];
    private int currentSpeedLimit;
    private float speed;
    void Start()
    {
        set_SpeedLImit(50);
    }

    public void set_SpeedLImit(int s)
    {
        switch (s){
            case 20:
                speedLimit[0].SetActive(true);
                speedLimit[1].SetActive(false);
                speedLimit[2].SetActive(false);
                currentSpeedLimit = 20;
                break;
            case 50:
                speedLimit[0].SetActive(false);
                speedLimit[1].SetActive(true);
                speedLimit[2].SetActive(false);
                currentSpeedLimit = 50;
                break;
            case 70:
                speedLimit[0].SetActive(false);
                speedLimit[1].SetActive(false);
                speedLimit[2].SetActive(true);
                currentSpeedLimit = 70;
                break;
        }
    }
    
    public void setNaviArrow(int arrowType)
    {
        if(arrowType < 3&&arrowType >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i != arrowType) NaviArrow[i].SetActive(false);
                else NaviArrow[i].SetActive(true);
            }
        }
        else
        {
            NaviArrow[0].SetActive(false);
            NaviArrow[1].SetActive(false);
            NaviArrow[2].SetActive(false);
        }
    }
    void FixedUpdate()
    {
        speed = Car.velocity.magnitude * 3.6f;
        CurrentV.text = speed.ToString("F1");
        if(speed > currentSpeedLimit)
        {
            CurrentV.color = new Color(255,0,0);
        }
        else if(speed > currentSpeedLimit - 5) CurrentV.color = new Color(255, 205, 0);
        else CurrentV.color = new Color(103, 205, 221);
    }
}
