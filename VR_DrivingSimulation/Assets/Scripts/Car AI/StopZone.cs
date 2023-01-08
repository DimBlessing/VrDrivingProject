using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    public GameObject AICar;
    public float brakingPower = 20000f;
    public float enginePower = 0f;
    private float startTorque = 0f;

    void Start()
    {
        //This is where the script finds the AI car and plugs it into the variable of this script.
        //AICar = GameObject.FindGameObjectWithTag("AI");

        //grab reference to user-set engine torque for default to reset to later
        startTorque = AICar.GetComponent<AIVehicle>().engineTorque; 
    }

    // Stop when the AI enters the trigger
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        AICar.GetComponent<AIVehicle> ().brakePower = (brakingPower);
        AICar.GetComponent<AIVehicle> ().engineTorque = (enginePower);
    }

    void OnTriggerExit(Collider other)
    {   
        Debug.Log("exit");
        AICar.GetComponent<AIVehicle> ().brakePower = 0f; 
        AICar.GetComponent<AIVehicle>().engineTorque = startTorque;
    }
}
