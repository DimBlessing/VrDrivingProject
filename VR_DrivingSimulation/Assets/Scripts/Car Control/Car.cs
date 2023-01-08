using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Car : CarController // FF
{
    protected override void Init()
    {
        MaxVelocity = 160f;
        MaxWheelAngle = 45;
        MaxMotorPower = 500f;//1000f; // MotorTorque
        MaxBrakePower = 20000f; // Brake
    }
    private void Awake()
    {
        Init();
        //InitKey();
        //InitGUI();
        RigidBodySetUp();
        InitWheel();
        InitConstValue();
        //InitFFSkidMarks();
        
        //InitBrakeLight();
    }
    private void FixedUpdate()
    {
        //G29Control();
        //KeyBoardControl();
        Movement();
        FFModeMovement();

        MoveVisualWheel(Wheels[0].Left_Wheel);
        MoveVisualWheel(Wheels[0].Right_Wheel);
        MoveVisualWheel(Wheels[1].Left_Wheel);
        MoveVisualWheel(Wheels[1].Right_Wheel);

        GUIUpdate();
        VisualSteering();
    }
}
