using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapOrRearCam : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject RearCam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ParkingZone"))
        {
            Minimap.SetActive(false);
            RearCam.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ParkingZone"))
        {
            Minimap.SetActive(true);
            RearCam.SetActive(false);
        }

    }
}
