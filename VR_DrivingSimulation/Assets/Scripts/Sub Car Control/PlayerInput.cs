using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Acceleration{
        get{
            return m_Acceleration;
        }
    }
    public float Steering{
        get{
            return m_Steering;
        }
    }

    float m_Acceleration;
    float m_Steering;

    bool FixedUpdateHappened;

    private bool accelerating = false;
    private bool breaking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    public float wheelDampening;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();

        if(accelerating){
            m_Acceleration = 1f;
            wheelDampening = 1f;  
        }
        else if(breaking){
            m_Acceleration = -0.5f;
            wheelDampening = 3500f;
        }
        else{
            m_Acceleration = 0f;
            wheelDampening = 5f;
        }

        if(turningLeft){    //- : 왼쪽
            m_Steering = -1f;
        }
        else if(!turningLeft && turningRight){  //+ : 오른쪽
            m_Steering = 1f;
        }
        else{
            m_Steering = 0f;
        }
    }
    public bool getBreakStat()
    {
        return breaking;
    }
    private void GetPlayerInput(){
        if(Input.GetKeyDown(KeyCode.W)){    //가속 w
            accelerating  = true;
        }
        if(Input.GetKeyUp(KeyCode.W)){
            accelerating = false;
        }

        if(Input.GetKeyDown(KeyCode.S)){    //브레이크 s
            breaking = true;
        }
        if(Input.GetKeyUp(KeyCode.S)){
            breaking = false;
        }

        if(Input.GetKeyDown(KeyCode.A)){    //좌회전 a
            turningLeft = true;
        }
        if(Input.GetKeyUp(KeyCode.A)){
            turningLeft = false;
        }

        if(Input.GetKeyDown(KeyCode.D)){    //우회전 d
            turningRight = true;
        }
        if(Input.GetKeyUp(KeyCode.D)){
            turningRight = false;
        }

    }
}
