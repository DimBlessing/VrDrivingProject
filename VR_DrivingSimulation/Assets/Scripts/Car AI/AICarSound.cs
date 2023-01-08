using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSound : MonoBehaviour
{
    Rigidbody CarRb;
    public AudioSource DriveSource;
    public AudioSource EngineSource;
    public int GearShiftLength = 20;
    public float PitchRange = 2.5f;
    public float PitchBoost = 0.8f;

    private float temp1;
    private int temp2;
    private float CarPitch;
    void Awake()
    {
        CarRb = GetComponent<Rigidbody>();
        CarPitch = 0.05f;
        EngineSource.volume = 0.1f;//0.3f;
        EngineSource.maxDistance = 25;
        DriveSource.volume = 0.2f;//0.5f;
        DriveSource.maxDistance = 25;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = CarRb.velocity.magnitude;
        temp1 = speed / GearShiftLength;
        temp2 = (int)temp1;

        float differ = temp1 - temp2;

        CarPitch = Mathf.Lerp(DriveSource.pitch, (PitchRange * differ) + PitchBoost, .01f);
        DriveSource.pitch = CarPitch;
        EngineSource.pitch = CarPitch;
    }
}
