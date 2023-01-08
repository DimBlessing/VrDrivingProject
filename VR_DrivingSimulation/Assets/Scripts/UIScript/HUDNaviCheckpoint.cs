using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDNaviCheckpoint : MonoBehaviour
{
    public Transform CheckPoint;
    public HUDScript HUD;
    [Header("0: Straight, 1: Right, 2: Left")]
    public int ArrowType = 0;
    [SerializeField]
    private bool is_ParkingPoint = false;
    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.setNaviArrow(ArrowType);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.setNaviArrow(ArrowType);
            HUD.Distance.text = Vector3.Distance(HUD.Car.position, CheckPoint.position).ToString("F0");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.Distance.text = "";
            if (is_ParkingPoint == false) HUD.setNaviArrow(0);
            else HUD.setNaviArrow(-1);
        }
    }
}
