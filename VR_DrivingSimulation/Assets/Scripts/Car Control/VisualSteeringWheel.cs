using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSteeringWheel : MonoBehaviour
{   
    //public Rigidbody car;
    public Transform steeringWheel;
    public CarController player = null;

    private float minAngle = -450f;
    private float maxAngle = 450f;
    private float steerAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //car = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        steerAngle = player.GetComponent<CarController>().VisualSteering();

        //Debug.Log(steerAngle);
        steeringWheel.localRotation = Quaternion.Euler(new Vector3(-0.003f, 0.401f, -steerAngle));
    }
}
