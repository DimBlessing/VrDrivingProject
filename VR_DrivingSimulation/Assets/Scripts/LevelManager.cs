using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    public struct Levels {
        public GameObject Level;
        public Transform LevelStartingPoint;
    }
    [SerializeField]
    public Levels[] LevelList = new Levels[3];
    public GameObject Player;
    private int SelectLevel;
    void Start()
    {
        //플레이어 오브젝트 설정
        Player = GameObject.FindGameObjectWithTag("Player");
        
        SelectLevel = GameSetting.Instance.Level;
        for (int i = 1; i < 4; i++)
        {
            if (i == SelectLevel)
            {
                LevelList[i - 1].Level.SetActive(true);
            }
            else Destroy(LevelList[i - 1].Level);
        }
        if(SelectLevel!= 0)GoStartingPoint(LevelList[SelectLevel - 1].LevelStartingPoint);
        
    }

    public void GoStartingPoint(Transform startingPoint)
    {
        Player.transform.position = startingPoint.position;
        Player.transform.rotation = startingPoint.rotation;
    }
}
