using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignStop : MonoBehaviour
{
    public GameObject AICar;
    public GameObject masterTraffic;
    //private BaseTrafficLight baseTraffic;

    public float brakingPower = 20000f;
    public float enginePower = 0f;
    private float startTorque = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        //startTorque = AICar.GetComponent<AIVehicle>().engineTorque;
        //baseTraffic = masterTraffic.GetComponent<BaseTrafficLight>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(masterTraffic.transform.GetChild(0).gameObject);
        if(masterTraffic.transform.GetChild(0).gameObject.activeSelf == true){  //master red light activated
            Debug.Log("go");
            //brakingPower = 0f;//10f;
            enginePower = 50f;

            AICar.GetComponent<AIVehicle> ().brakePower = 0f;
            AICar.GetComponent<AIVehicle> ().engineTorque = (enginePower);
        }
        else{
            Debug.Log("stop");
            //brakingPower = 20000f;
            enginePower = 0f;

            AICar.GetComponent<AIVehicle> ().brakePower = (brakingPower);
            AICar.GetComponent<AIVehicle> ().engineTorque = (enginePower);
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
        switch(other.gameObject.tag){
            case "RedLightOver": case "RedLightSlow":
                brakingPower = 100000f;
                enginePower = 0f;
                //brakingPower = 10f;
                //enginePower = 200f;
            break;
            default:
                brakingPower = 10f;
                enginePower = 200f;
                //brakingPower = 20000f;
                //enginePower = 0f;
            break;
        }
        AICar.GetComponent<AIVehicle> ().brakePower = (brakingPower);
        AICar.GetComponent<AIVehicle> ().engineTorque = (enginePower);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("exit stop");
        AICar.GetComponent<AIVehicle> ().brakePower = 0f; 
        AICar.GetComponent<AIVehicle>().engineTorque = 200f;//startTorque;
    }*/
}
