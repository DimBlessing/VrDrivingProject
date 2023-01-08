using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignPedestrian : MonoBehaviour
{
    public GameObject AIPerson;
    public Animator AIMove;
    public GameObject masterTraffic;

    public RuntimeAnimatorController walk;
    public RuntimeAnimatorController idle;

    // Start is called before the first frame update
    void Start()
    {
        AIMove = AIPerson.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(masterTraffic.transform.GetChild(0).gameObject.activeSelf == true){
            Debug.Log("person go");
            AIMove.runtimeAnimatorController = walk;
        }    
        else{
            Debug.Log("person stop");
            AIMove.runtimeAnimatorController = idle;
        }
    }
}
