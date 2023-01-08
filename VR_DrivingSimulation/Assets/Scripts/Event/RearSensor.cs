using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearSensor : MonoBehaviour
{
    public SensorSoundManager SensorManager;
    public int SensorLevel;
    private int ObjectInTrigger = 0;

    private void FixedUpdate()
    {
        if (ObjectInTrigger > 0 && SensorManager.getNowSound() < SensorLevel) SensorManager.PlaySensorSound(SensorLevel);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Untagged") || other.CompareTag("CollisionEvent"))
        {
            ObjectInTrigger++;
            if (SensorManager.getNowSound() < SensorLevel) SensorManager.PlaySensorSound(SensorLevel);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Untagged") || other.CompareTag("CollisionEvent"))
        {
            if (ObjectInTrigger > 1) ObjectInTrigger--;
            else
            {
                ObjectInTrigger = 0;
                SensorManager.PlaySensorSound(SensorLevel - 1);
            }
        }
        
    }
}
