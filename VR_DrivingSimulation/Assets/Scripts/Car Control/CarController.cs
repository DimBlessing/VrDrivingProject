using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour // Normal Base Car Class
{
    #region Logitech G29 Controller
    protected LogitechGSDK.LogiControllerPropertiesData properties; // Logitech Controller
    protected LogitechGSDK.DIJOYSTATE2ENGINES controller;
    #endregion

    #region Left, Right Wheel
    protected class WheelInfo // Left and Right Wheels
    {
        public WheelCollider Left_Wheel;
        public WheelCollider Right_Wheel;
    }
    #endregion

    #region Left, Right Skid Mark
    protected class SkidMark // Left and Right Skid Marks
    {
        public TrailRenderer Left_Skid;
        public TrailRenderer Right_Skid;
    }
    #endregion

    #region Car Movement Values
    protected float Steering = 0f; // Handle Angle
    protected float Motor = 0f; // Motor Power
    protected float Brake = 0f; // Brake Power
    protected float myVeloctiy = 0f; // current speed
    protected bool FrontGear = false; // Front Gear
    protected bool BackGear = false; // Rear Gear
    protected float VisualSteeringAngle = 0f;
    #endregion

    #region Maximum Movement Values
    protected int MaxWheelAngle = 45; // Wheels can rotate between maximum value
    protected float MaxMotorPower = 500f; //1000f; // It means motorTorque
    protected float MaxBrakePower = 20000f; // Brake Maximum power
    protected float MaxVelocity = 160f; // Max speed km/s
    #endregion

    #region The Const Value For Using Controller
    protected int MaxHandleAngle = 450; // G29 Real Controller Max Angle (One Side)
    protected float Int2HandleAngle; // Int => Handle Angle
    protected float Handle2WheelAngle; // Handle Angle => Wheel Angle
    protected float Int2Throttle; // Int => Throttle Pedal value
    protected float Int2Brake; // Int => Brake pedal value
    #endregion

    #region Wheel Collider Pos, Rot For Visual Wheel Movement
    protected Vector3 colliderWorldPos; // Wheel collider position
    protected Quaternion colliderWorldRot; // Wheel collider rotation
    #endregion

    #region 
    protected float speedFactor = 0f; // current speed / max speed => percentage
    protected float rotationAngle = 0f; // speedometer arrow pointer angle
    protected Image arrowPointer; // GUI Arrow
    protected TextMeshProUGUI speedUI; // Display Velocity GUI
    #endregion

    #region The Variable Of Car Rigidbody
    protected Rigidbody rigidBody; // Car rigidbody
    public Rigidbody GetRigidbody { get { return rigidBody; } }
    protected GameObject centerOfMass; // Car center of mass
    #endregion

    #region Lists Of The Car Component
    protected List<WheelInfo> Wheels = new List<WheelInfo>(); // Wheels List
    protected SkidMark Skids = new SkidMark(); // Skid Marks
    protected GameObject brakeLight = null; // Back light object
    protected GameObject visualWheel = null; // visual wheels
    #endregion

    #region Initialize Base Information Called Only Once
    protected virtual void Init()
    {
        MaxVelocity = 160f;
        MaxWheelAngle = 45;
        MaxMotorPower = 500f; //1000f; // It means motorTorque
        MaxBrakePower = 20000f; // Brake
    } // Init State Value

    #region Never Use
    /*
    protected virtual void InitKey() 
    {
        switch(GameSetting.Instance.CurrentController)
        {
            case 0:
                {
                    // Init KeyBoard
                    InputManager.Instance.KeyAction -= KeyBoardControl;
                    InputManager.Instance.KeyAction += KeyBoardControl;
                    break;
                }
            case 1:
                {
                    // Init Controller
                    InputManager.Instance.KeyAction -= G29Control;
                    InputManager.Instance.KeyAction += G29Control;
                    break;
                }
            default:
                {
                    // Init KeyBoard
                    InputManager.Instance.KeyAction -= KeyBoardControl;
                    InputManager.Instance.KeyAction += KeyBoardControl;
                    break;
                }
        }
    }// Initialize Control Method >>> NOT USE BECAUSE OF THE BUG
    */
    #endregion
    /*protected virtual void InitGUI()
    {
        speedUI = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();
        arrowPointer = GameObject.Find("Arrow").GetComponent<Image>();
    }*/ //Init GUI
    protected virtual void RigidBodySetUp()
    {
        rigidBody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.FindGameObjectWithTag("CM").gameObject;
        rigidBody.centerOfMass = centerOfMass.transform.localPosition;
    } // Get Rigidbody and center of mass
    protected virtual void InitWheel()
    {        
        //Logitech SDK Load timing condition implement
        //if()
        LogitechGSDK.LogiSteeringInitialize(false);

        WheelInfo Front = new WheelInfo();
        WheelInfo Back = new WheelInfo();

        Wheels.Add(Front);
        Wheels.Add(Back);

        // Foward Wheels
        Wheels[0].Left_Wheel = GameObject.FindGameObjectWithTag("LFW").GetComponent<WheelCollider>();
        Wheels[0].Right_Wheel = GameObject.FindGameObjectWithTag("RFW").GetComponent<WheelCollider>();
        // Backward Wheels
        Wheels[1].Left_Wheel = GameObject.FindGameObjectWithTag("LBW").GetComponent<WheelCollider>();
        Wheels[1].Right_Wheel = GameObject.FindGameObjectWithTag("RBW").GetComponent<WheelCollider>();

    } // Initialize 4 Wheels
    /*protected virtual void InitFFSkidMarks()
    {
        // Foward Wheels Skid Marks
        Skids.Left_Skid = GameObject.FindGameObjectWithTag("LFW").GetComponentInChildren<TrailRenderer>(); // Left
        Skids.Right_Skid = GameObject.FindGameObjectWithTag("RFW").GetComponentInChildren<TrailRenderer>(); // Right
        Skids.Left_Skid.emitting = false; // Visible false on start
        Skids.Right_Skid.emitting = false;
    } // Initialize FF Type Skid marks
    protected virtual void InitRRSkidMarks()
    {
        // Foward Wheels Skid Marks
        Skids.Left_Skid = GameObject.FindGameObjectWithTag("LBW").GetComponentInChildren<TrailRenderer>();
        Skids.Right_Skid = GameObasect.FindGameObjectWithTag("RBW").GetComponentInChildren<TrailRenderer>();
        Skids.Left_Skid.emitting = false;
        Skids.Right_Skid.emitting = false;
    }*/ // Initialize RR Type Skid marks
    protected virtual void InitBrakeLight()
    {
        // Brake BackLight
        brakeLight = GameObject.FindGameObjectWithTag("BackLight");
        brakeLight.SetActive(false);
    } // Initialize Brake Light
    protected virtual void InitConstValue()
    {
        Int2HandleAngle = 32767f / MaxHandleAngle; // 32767 / 450 >> Convert Int to Handle Degree
        Handle2WheelAngle = MaxHandleAngle / MaxWheelAngle; // 450 / 45 >> Convert Handle Degree to Wheel Degree
        Int2Throttle = 65534 / MaxMotorPower; // Convert Int to Throttle pedal value
        Int2Brake = 65534 / MaxBrakePower; // Convert Int to Brake pedal value
    } // Initalize some values used on G29 Wheel
    #endregion

    #region The Real Car Movement Operated By My Controller
    protected virtual void MoveVisualWheel(WheelCollider wheel)
    {
        wheel.GetWorldPose(out colliderWorldPos, out colliderWorldRot);
        visualWheel = wheel.transform.GetChild(3).gameObject;
        visualWheel.transform.position = colliderWorldPos;
        visualWheel.transform.rotation = colliderWorldRot;
    } // Move Visual Real Wheel
    protected virtual void KeyBoardControl()
    {
        Steering = Input.GetAxis("Horizontal") * MaxWheelAngle; // Keyboard Input value
        Motor = Input.GetAxis("Vertical") * MaxMotorPower;

        if (rigidBody.velocity.magnitude * 3.6f > MaxVelocity) // Limit Max Velocity
        {
            Debug.Log("max");
            Motor = 0f;
        }

        if (Input.GetKey(KeyCode.Space)) // When Brake ON
        {
            Brake = MaxBrakePower; // Brake
            Motor = 0f; // Motor is 0
            if (rigidBody.velocity.magnitude * 3.6f > 70) // If velocity is over 70, skid marks visible true
            {
                //Skids.Left_Skid.emitting = true;
                //Skids.Right_Skid.emitting = true;
            }
            //brakeLight.SetActive(true); // Brake Light ON
        }
        else // When Brake OFF
        {
            Brake = 0f;
            //Skids.Left_Skid.emitting = false;
            //Skids.Right_Skid.emitting = false;
            //brakeLight.SetActive(false);
        }

        //if (Input.GetKey(KeyCode.S)) brakeLight.SetActive(true); // When Input S(Back) Brake Light ON
       // else brakeLight.SetActive(false);

    } // Keyboard Input
    protected virtual void G29Control()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            controller = LogitechGSDK.LogiGetStateUnity(0); // Logitech G 29 Wheel

            LogitechGSDK.LogiPlaySpringForce(0, 0, 40, 30); // ForceFeedback Setting

            if (FrontGear)
            {
                //Motor = Mathf.Round((-controller.lY + Mathf.Abs(controller.lY)) / Int2Throttle); // Throttle
                Motor = (Mathf.Round((-controller.lY + Mathf.Abs(controller.lY)) / Int2Throttle)) / 4; // Throttle
            }
            else if (BackGear)
            {
                Motor = -Mathf.Round((-controller.lY + Mathf.Abs(controller.lY)) / Int2Throttle); // Throttle
            }
            else Motor = 0f;

            Steering = controller.lX / Int2HandleAngle / Handle2WheelAngle; // Handle
            Brake = Mathf.Round((-controller.lRz + Mathf.Abs(controller.lRz)) / Int2Brake); // Brake

            //??????????????? ?????? ????????? ?????? ???????
            FrontGear = true;
            BackGear = false;
            Debug.Log("1 st Gear Input");
            for (int i = 0; i < 128; i++) // Gear Button Input
            {
                if (controller.rgbButtons[i] == 128)
                {
                    /*if (i == 11) // Forward gear 12
                    {
                        FrontGear = true;
                        BackGear = false;
                        Debug.Log("1 st Gear Input");
                    }
                    else if (i == 10) // Back gear 18
                    {
                        FrontGear = false;
                        BackGear = true;
                        Debug.Log("Backward Gear Input");
                    }
                    */
                    if(i == 10){
                        FrontGear = false;
                        BackGear = true;
                        Debug.Log("Backward Gear Input");
                    }
                    /*else{
                        FrontGear = true;
                        BackGear = false;
                        Debug.Log("1 st Gear Input");
                    }*/
                }
            }
            //??????????????? ????????? ????????? ??????????
            /*if (controller.rgbButtons[11] != 128 && controller.rgbButtons[10] != 128) // Gear N
            {
                FrontGear = false;
                BackGear = false;
            }*/

            if (Brake > 0.1f) // If Brake ON
            {
                //brakeLight.SetActive(true); // BackLight ON
            }
            else
            {
                //brakeLight.SetActive(false);
            }
        }
        else if (!LogitechGSDK.LogiIsConnected(0) && GameSetting.Instance.CurrentController == 1)
        {
            Debug.Log("LOGITECH DEVICE NOT CONNECTED");
        }
        else
        {
            if(GameSetting.Instance.CurrentController == 1)
            Debug.Log("Device Connected, but some errors occured");
        }
    } // G29 Input
    protected virtual void Movement() // Total Movement by my Controller ID
    {
        if(GameManager.Instance.IsGaming) // When Input is enabled
        {
            switch (GameSetting.Instance.CurrentController) // controller type
            {
                case 0: { KeyBoardControl(); break; }
                case 1: { G29Control(); break; }
                default: { KeyBoardControl(); break; } // default : keyboard
            }
        }
    }
    protected virtual void FFModeMovement()
    {
        // ========== FF ========= //
        // Steer
        Wheels[0].Left_Wheel.steerAngle = Steering;
        Wheels[0].Right_Wheel.steerAngle = Steering;
        // Motor
        Wheels[0].Left_Wheel.motorTorque = Motor;
        Wheels[0].Right_Wheel.motorTorque = Motor;
        // Brake
        Wheels[0].Left_Wheel.brakeTorque = Brake;
        Wheels[0].Right_Wheel.brakeTorque = Brake;
    } // FF Car Setting
    protected virtual void RRModeMovement()
    {
        // ========== RR ========= //
        // Steer
        Wheels[0].Left_Wheel.steerAngle = Steering;
        Wheels[0].Right_Wheel.steerAngle = Steering;
        // Motor
        Wheels[1].Left_Wheel.motorTorque = Motor;
        Wheels[1].Right_Wheel.motorTorque = Motor;
        // Brake
        Wheels[1].Left_Wheel.brakeTorque = Brake;
        Wheels[1].Right_Wheel.brakeTorque = Brake;
    }// RR Car Setting
    #endregion

    #region Speedometer Update Function
    protected virtual void GUIUpdate()
    {
        myVeloctiy = rigidBody.velocity.magnitude * 3.6f; // Convert m/s -> km/s with multiply 3.6
        //speedUI.text = myVeloctiy.ToString("000"); // Truncate
        speedFactor = Mathf.Abs(rigidBody.velocity.magnitude * 3.6f / MaxVelocity);
        rotationAngle = Mathf.Lerp(0, 315, speedFactor);

        
        //arrowPointer.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -rotationAngle));
        Debug.Log("SteeringAngle : " + Steering + "  MotorTorque : " + Motor + "  BrakePower : " + Brake + "  RPM : " + Wheels[0].Left_Wheel.rpm + "  Velocity : " + rigidBody.velocity.magnitude * 3.6f);
    } // Speedometer Update
    #endregion

    public bool getGearStat()
    {
        return BackGear;
    }
    public float VisualSteering()
    {
        VisualSteeringAngle = controller.lX / Int2HandleAngle;
        //Debug.Log(VisualSteeringAngle);

        return VisualSteeringAngle;
    }

    private void OnDestroy()
    {
        if (LogitechGSDK.LogiIsConnected(0)) // When Destroy Object, the controller shut down together and alert
        {
            Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
        }
    } // ShutDown Controller
}
