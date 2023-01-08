using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSoundManager : MonoBehaviour
{
    private int now_sound = 0;
    public AudioSource[] SensorSounds = new AudioSource[3];

    public void PlaySensorSound(int SensorNum)
    {
        switch (SensorNum)
        {
            case 0:
                now_sound = 0;
                SensorSounds[0].Stop();
                SensorSounds[1].Stop();
                SensorSounds[2].Stop();
                break;
            case 1:
                now_sound = 1;
                SensorSounds[0].Play();
                SensorSounds[1].Stop();
                SensorSounds[2].Stop();
                break;
            case 2:
                now_sound = 2;
                SensorSounds[0].Stop();
                SensorSounds[1].Play();
                SensorSounds[2].Stop();
                break;
            case 3:
                now_sound = 3;
                SensorSounds[0].Stop();
                SensorSounds[1].Stop();
                SensorSounds[2].Play();
                break;
        }
    }
    public int getNowSound()
    {
        return now_sound;
    }
}
