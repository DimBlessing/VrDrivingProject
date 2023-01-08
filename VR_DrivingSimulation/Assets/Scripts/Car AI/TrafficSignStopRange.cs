using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TrafficSignStop 스크립트 활성/비활성화 트리거
public class TrafficSignStopRange : MonoBehaviour
{
    //public GameObject AICar;
    //public float brakingPower = 0f;
    //public float enginePower = 0f;
    //private float startTorque = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //startTorque = AICar.GetComponent<AIVehicle>().engineTorque;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
    void OnTriggerEnter(Collider other){
        if(other.tag == "AI"){
            Debug.Log("test enter");
            other.GetComponent<TrafficSignStop>().enabled = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "AI"){
            Debug.Log("test exit");
            other.GetComponent<TrafficSignStop>().enabled = false;

            other.GetComponent<AIVehicle>().engineTorque = 50f;
            /*AICar.GetComponent<AIVehicle> ().brakePower = (brakingPower);
            AICar.GetComponent<AIVehicle> ().engineTorque = (startTorque);*/
        }
    }
}
