using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subAccel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "AI"){
            other.GetComponent<AIVehicle>().brakePower = 0f;
            other.GetComponent<AIVehicle>().engineTorque = 30f;
        }
    }
}
