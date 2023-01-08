using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeZone : MonoBehaviour {

    //public GameObject AICar;
    public float brakingPower = 1f;
    public float enginePower = 20f;
    private float startTorque = 0f;

    //private float[] beforeTorque;

    void Start()
    {
        //This is where the script finds the AI car and plugs it into the variable of this script.
        //AICar = GameObject.FindGameObjectWithTag("AI");

        //grab reference to user-set engine torque for default to reset to later
        //startTorque = AICar.GetComponent<AIVehicle>().engineTorque;
        //beforeTorque = new float[transform.childCount];

    }

    // Hit the brakes when the AI enters the trigger
    void OnTriggerEnter(Collider other)
    {   
        if(other.tag == "AI"){
            Debug.Log("enter brake");
            startTorque = other.GetComponent<AIVehicle>().engineTorque;
            other.GetComponent<AIVehicle>().brakePower = (brakingPower);
            other.GetComponent<AIVehicle>().engineTorque = (enginePower);
        }
        /*AICar.GetComponent<AIVehicle> ().brakePower = (brakingPower);
        AICar.GetComponent<AIVehicle> ().engineTorque = (enginePower);
        */
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "AI"){
            Debug.Log("exit brake");
            
            //other.GetComponent<AIVehicle>()
            other.GetComponent<AIVehicle>().brakePower = 0f;
            other.GetComponent<AIVehicle>().engineTorque = startTorque;
        }
        /*Debug.Log("exit brake");
        AICar.GetComponent<AIVehicle> ().brakePower = 0f; 
        AICar.GetComponent<AIVehicle>().engineTorque = startTorque;
        */
    }
}
