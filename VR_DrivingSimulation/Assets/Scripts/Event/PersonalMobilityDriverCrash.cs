using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalMobilityDriverCrash : MonoBehaviour
{
    public GameObject Driver;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Driver.GetComponent<Rigidbody>() == true) Destroy(Driver.GetComponent<Rigidbody>());
            Driver.AddComponent<Rigidbody>();
            Rigidbody DriverRb = Driver.GetComponent<Rigidbody>();
            DriverRb.isKinematic = false;
            
            Driver.GetComponent<BoxCollider>().enabled = true;
            Driver.GetComponent<Rigidbody>().AddExplosionForce(15000f * collision.gameObject.GetComponent<ScoreManager>().getCrashBeforeSpeed(), transform.position, 200f, 1000f);
        }
    }
}
