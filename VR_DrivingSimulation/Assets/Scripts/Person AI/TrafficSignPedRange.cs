using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignPedRange : MonoBehaviour
{
    //public GameManager AIPerson;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    void OnTriggerEnter(Collider other){
        if(other.tag == "AI"){
            other.GetComponent<TrafficSignPedestrian>().enabled = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "AI"){
            other.GetComponent<TrafficSignPedestrian>().enabled = false;
        }
    }
}
