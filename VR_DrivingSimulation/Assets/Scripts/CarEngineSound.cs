using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    Rigidbody CarRb;
    public AudioSource EngineDriveSource, EngineSource_lowFront, EngineSource_highFront, EngineSource_lowRear, EngineSource_highRear, EngineSource_start, EngineSource_Stop;
    public int GearShiftLength = 20;
    public float PitchRange = 2.5f;
    public float PitchBoost = 0.8f;

    private float temp1;
    private int temp2;
    void Awake()
    {
        CarRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = CarRb.velocity.magnitude;

        temp1 = speed / GearShiftLength;
        temp2 = (int)temp1;
        if (EngineSource_lowFront.isPlaying == false && temp2 < 3)
        {
            EngineSource_lowFront.Play();
            EngineSource_lowRear.Play();
            EngineSource_highFront.Stop();
            EngineSource_highRear.Stop();
        }
        if (EngineSource_highFront.isPlaying == false && temp2 >= 3)
        {
            EngineSource_lowFront.Stop();
            EngineSource_lowRear.Stop();
            EngineSource_highFront.Play();
            EngineSource_highRear.Play();
        }
        float differ = temp1 - temp2;
        float CarPitch = Mathf.Lerp(EngineSource_lowFront.pitch, (PitchRange * differ) + PitchBoost, .01f);
        if (EngineSource_lowFront.isPlaying == true)
        {
            EngineSource_lowFront.pitch = CarPitch;
            EngineSource_lowRear.pitch = CarPitch;
        }
        if (EngineSource_highFront.isPlaying == true)
        {
            EngineSource_highFront.pitch = CarPitch;
            EngineSource_highRear.pitch = CarPitch;
        }
        EngineDriveSource.pitch = CarPitch;
    }

    public void EngineStop()
    {
        EngineDriveSource.Stop();
        EngineSource_lowFront.Stop();
        EngineSource_highFront.Stop();
        EngineSource_lowRear.Stop();
        EngineSource_highRear.Stop();
        EngineSource_Stop.Play();
    }
}
