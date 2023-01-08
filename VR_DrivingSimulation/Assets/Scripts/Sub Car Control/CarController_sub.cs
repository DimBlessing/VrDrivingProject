using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_sub : MonoBehaviour
{
    private PlayerInput inputManager;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    private float strengthCoeffiecient = 9000000f; //높을수록 차량속도 빨라짐
    private float maxTurn = 50f; //차량 회전각

    private float speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<PlayerInput>();
        Debug.Log(inputManager);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WheelCollider wheel in throttleWheels){
            wheel.motorTorque = strengthCoeffiecient * Time.deltaTime * inputManager.Acceleration;
            wheel.wheelDampingRate = inputManager.wheelDampening;

            //speed += 0.05f;
        }
        foreach(WheelCollider wheel in steeringWheels){
            wheel.steerAngle = maxTurn * inputManager.Steering;
            wheel.wheelDampingRate = inputManager.wheelDampening;
            
        }   
    }
}


//가속 : wheeldampingRate = 400
//브레이크 : 1200
//정지 : 5