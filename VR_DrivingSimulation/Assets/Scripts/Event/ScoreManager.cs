using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SlimUI.ModernMenu;
using TMPro;

public enum Parking_Type
{
    Front_Parking, Rear_Parking, Both
}
public class ScoreManager : MonoBehaviour
{
    [Header("ScoreSetting")]
    public int init_score = 100;//최초 점수(보통은 100)
    public int minimum_score_for_pass = 75;
    [Header("Parking")]
    public Parking_Type ParkingWay;
    public Transform Car_Front, Car_Back;
    [Header("PlayerObject")]
    public Rigidbody PlayerCar;
    [Header("HUD Object")]
    public HUDScript HUD;
    [Header("ResultMenu script is in UICamera")]
    public GameObject ResultMenu;
    [Space(5f)]
    [Header("EventList Object in ResultUI Object")]
    public GameObject EventList;
    [HideInInspector]
    public DrivingEvent.Feedback_List ScoreBoard;
    [Space(5f)]
    [Header("Not Event Feedback, Overall Feedback Title & Contents Text")]
    public TMP_Text Feedback_Title;
    public TMP_Text Feedback_Contents;
    public TMP_Text Feedback_RedLight;
    public TMP_Text Feedback_Control;
    [Header("Result Text: Pass/Non-Pass")]
    public TMP_Text ResultText;
    [Header("Show Score In Playing")]
    public TMP_Text Now_Score;
    [SerializeField]
    private int parking_check;
    public AudioSource CrashSoundSource;
    public AudioSource HumanCrashSoundSource;
    [SerializeField]
    private GameObject WarningCanv;
    private float speed;
    private int parkingWay;
    private int RedLightOverCount = 0;
    private int RedLightSlowCount = 0;
    private bool is_CircRoadStop = false;
    private bool is_Warning = false;
    private float velocity_crashbefore, velocity_after, velocity_crashObj;
    private int speedLimit = 50;
    private int speedingCnt = 0;

    void Start()
    {
        RedLightOverCount = 0;
        RedLightSlowCount = 0;
        speed = 0.0f;
        speedLimit = 50;
        ScoreBoard.init();
        ScoreBoard.score = init_score;
        parking_check = 0;
        WarningCanv.SetActive(false) ;
        if (ParkingWay.ToString() == "Both") parkingWay = 0;
        else if (ParkingWay.ToString() == "Front_Parking") parkingWay = 1;
        else parkingWay = 2;
    }
    
    void Update()
    {
        Now_Score.text = ScoreBoard.score.ToString();
        if(ScoreBoard.score <= 0)
        {
            ScoreBoard.score = 0;
            OpenResultMenu(1);
        }
        if (parking_check == 4) {
            speed = PlayerCar.velocity.sqrMagnitude;
            if (speed < 0.0001f) {
                PlayerCar.GetComponent<CarEngineSound>().EngineStop();
                OpenResultMenu(2);
            }
        }
        if(PlayerCar.velocity.magnitude * 3.6 > speedLimit && is_Speeding_routine == false)
        {
            StartCoroutine("SpeedingCheck");
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OpenResultMenu(2);
        }
    }
    public int getScore()
    {
        return ScoreBoard.score;
    }
    public void setScore(int n)
    {
        ScoreBoard.score -= n;
    }
    public void OpenResultMenu(int trigger)
    {
        CheckSpeedingEvent();
        if (ScoreBoard.score >= 75) ResultText.text = "합격입니다!";
        else ResultText.text = "불합격입니다.";
        feedBackUpdate();
        ResultMenu.GetComponent<ResultMenu>().ScoreText.text = (ScoreBoard.score.ToString());
        ResultMenu.GetComponent<ResultMenu>().setEndTrriger(trigger);
    }
    public void CrashSetScoreZero()
    {
        ScoreBoard.score = 0;
        WarningCanv.SetActive(false);
    }

    public void setFeedList(DrivingEvent.Feedback_List EventScore)
    {
        ScoreBoard.score -= EventScore.score;
        ScoreBoard.negligent += EventScore.negligent;
        ScoreBoard.poor_driving += EventScore.poor_driving;
        ScoreBoard.deceleration += EventScore.deceleration;
        ScoreBoard.safety_distance += EventScore.safety_distance;
    }

    public DrivingEvent.Feedback_List getScoreBoard() 
    {
        return ScoreBoard;
    }

    void check_AfterVelocity()
    {
        velocity_after = PlayerCar.velocity.magnitude*3.6f;
    }

    private bool is_Speeding_routine = false;
    IEnumerator SpeedingCheck()
    {
        is_Speeding_routine = true;
        ScoreBoard.score -= 1;
        speedingCnt++;
        yield return new WaitForSeconds(1f);
        is_Speeding_routine = false;
    }

    void CheckSpeedingEvent()
    {
        if(speedingCnt > 0)
        {
            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("제한 속도 위반 / -" + speedingCnt +"점", "제한 속도를 초과하셨습니다.");
            EventList.GetComponent<EventBtnActive>().event_btn_active();
            speedingCnt = 0;
        }
    }
    public float getCrashBeforeSpeed() { return velocity_crashbefore; }
    
    private Rigidbody crashObj;
    private void OnTriggerEnter(Collider other)
    {
        if(is_Warning == false)
        {
            switch (other.gameObject.tag)
            {
                case "CollisionEvent":
                    int eventScore = other.GetComponent<DrivingEvent>().getScore();
                    if (ScoreBoard.score < eventScore) eventScore = ScoreBoard.score;
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / -" + eventScore.ToString() + "점", other.GetComponent<DrivingEvent>().event_feedback);
                    EventList.GetComponent<EventBtnActive>().event_btn_active();
                    setFeedList(other.GetComponent<DrivingEvent>().getEventScore());
                    break;
                case "Eve_Crash":
                    velocity_crashbefore = PlayerCar.velocity.magnitude*3.6f;
                    print("velocity before: "+velocity_crashbefore);
                    break;
                case "AI":
                    crashObj = other.gameObject.GetComponent<Rigidbody>();
                    if(crashObj.mass > 500)
                    {
                        Invoke("check_AfterVelocity", 0.01f);
                        velocity_crashObj = crashObj.velocity.magnitude * 3.6f;
                        if (Mathf.Abs(velocity_after - velocity_crashbefore) > 8f || (velocity_crashObj - velocity_after > 4f))
                        {
                            is_Warning = true;
                            if (crashObj.mass * velocity_crashObj > PlayerCar.mass * velocity_after) PlayerCar.AddExplosionForce(200000f, transform.position, 200.0f, 5000f);
                            else crashObj.AddExplosionForce(200000f, transform.position, 200.0f, 5000f);
                            CrashSoundSource.Play();

                            WarningCanv.SetActive(true);
                            eventScore = other.GetComponent<DrivingEvent>().getScore();
                            if (ScoreBoard.score < eventScore) eventScore = ScoreBoard.score;
                            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("심각한 충돌 사고 발생 / 실격", "다른 차량 또는 이동 장치와 큰 충격의 발생이 감지되어 즉시 실격처리 되었습니다.\n" + other.GetComponent<DrivingEvent>().event_feedback);
                            EventList.GetComponent<EventBtnActive>().event_btn_active();
                            setFeedList(other.GetComponent<DrivingEvent>().getEventScore());
                            Invoke("CrashSetScoreZero", 2.0f);
                        }
                        else
                        {
                            eventScore = other.GetComponent<DrivingEvent>().getScore();
                            if (ScoreBoard.score < eventScore) eventScore = ScoreBoard.score;
                            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / -" + eventScore.ToString() + "점", other.GetComponent<DrivingEvent>().event_feedback);
                            EventList.GetComponent<EventBtnActive>().event_btn_active();
                            setFeedList(other.GetComponent<DrivingEvent>().getEventScore());
                        }
                    }
                    else if(crashObj.mass < 150)
                    {
                        crashObj.isKinematic = false;
                        is_Warning = true;
                        crashObj.AddExplosionForce(1500f * velocity_crashbefore, transform.position, 200.0f, 200f);
                        HumanCrashSoundSource.Play();
                        WarningCanv.SetActive(true);
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("보행자 충돌 사고 발생 / 실격", "사람과의 충돌이 감지되어 즉시 실격처리 되었습니다.\n보행자의 경우 갑작스럽게 도로로뛰어들 수 있고, 차량보다 잘 보이지 않기에 운전 중에는 항상 주변 보행자들의 행동에 주의를 기울여야 합니다.");
                        EventList.GetComponent<EventBtnActive>().event_btn_active();
                        Invoke("CrashSetScoreZero", 2.0f);
                    }
                    else
                    {
                        is_Warning = true;
                        eventScore = other.GetComponent<DrivingEvent>().getScore();
                        if (ScoreBoard.score < eventScore) eventScore = ScoreBoard.score;
                        crashObj.AddExplosionForce(1350f * velocity_crashbefore, transform.position, 200.0f, 200f);
                        HumanCrashSoundSource.Play();
                        WarningCanv.SetActive(true);
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / 실격", other.GetComponent<DrivingEvent>().event_feedback);
                        EventList.GetComponent<EventBtnActive>().event_btn_active();
                        Invoke("CrashSetScoreZero", 2.0f);
                    }
                    break;
                case "Destination":
                    if (parking_check == 0)
                    {
                        Vector3 triggerVector = other.gameObject.transform.position;
                        Vector3 temp = triggerVector - Car_Back.position;
                        float backDistance = temp.sqrMagnitude;
                        temp = triggerVector - Car_Front.position;
                        float frontDistance = temp.sqrMagnitude;
                        bool is_goahead = frontDistance < backDistance;
                        switch (parkingWay)
                        {
                            case 0:
                                break;
                            case 1:
                                if (is_goahead == false) return;
                                break;
                            case 2:
                                if (is_goahead == true) return;
                                break;
                        }
                    }
                    parking_check++;
                    break;
                case "RedLightOver":
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("신호 위반 / -15점", "신호를 위반하셨습니다.\n항상 전방 신호 상태를 주시하시고, 초록 신호로 바뀐지 오래 되었다면 신호 변경에 대해서도 고려하셔야 합니다.\n교차로에서는 주변 보행자 신호의 상태를 통해 다음 운전자 신호 변화를 예측하는 것도 좋은 방법입니다.");
                    EventList.GetComponent<EventBtnActive>().event_btn_active();
                    ScoreBoard.score -= 15;
                    RedLightOverCount += 1;
                    Destroy(other);
                    break;
                case "RedLightSlow":
                    if (PlayerCar.velocity.sqrMagnitude > 25f)
                    {
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("감속 조절 미흡 / -5점", "신호가 붉은색으로 변한 것을 확인했다면 충분한 거리를 두고 감속을 시작해야 합니다.\n특히 횡단보도 근처에서는 언제든 돌발상황이 발생할 수 있으므로 늘 신호와 주변 상황에 유의하여 운전할 수 있어야 합니다.");
                        EventList.GetComponent<EventBtnActive>().event_btn_active();
                        ScoreBoard.score -= 5;
                        RedLightSlowCount += 1;
                    }
                    Destroy(other);
                    break;
                case "CircRoadStop":
                    is_CircRoadStop = false;
                    break;

            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CircRoadStop":
                if (PlayerCar.velocity.sqrMagnitude < 9f) is_CircRoadStop = true;
                break;
            case "ParkingZone":
                speedLimit = 20;
                HUD.set_SpeedLImit(speedLimit);
                break;
            case "HighWay":
                speedLimit = 70;
                HUD.set_SpeedLImit(speedLimit);
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CollisionEvent":
                break;
            case "Destination":
                if(parking_check > 0) parking_check--;
                break;
            case "CircRoadStop":
                if (is_CircRoadStop == false)
                {
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("회전 도로 정지 / -10점", "회전 도로 진입 전에는 잠시 멈춘 후 도로 상황을 살펴보시고 다른 차량을 확인하신 후에 신중하게 진입하시는 것을 권장드립니다.");
                    EventList.GetComponent<EventBtnActive>().event_btn_active();
                    ScoreBoard.score -= 10;
                }
                Destroy(other);
                break;
            case "ParkingZone":
                speedLimit = 50;
                HUD.set_SpeedLImit(speedLimit);
                break;
            case "HighWay":
                speedLimit = 50;
                HUD.set_SpeedLImit(speedLimit);
                break;
        }
    }




    public int feedbackTitle()
    {
        int first;
        first = 0;
        var feed_list = new List<int> { ScoreBoard.negligent, ScoreBoard.poor_driving, ScoreBoard.deceleration, ScoreBoard.safety_distance };
        for(int i = 1; i < ScoreBoard.getNumOfFeed(); i++)
        {
            if(feed_list[i] > feed_list[first])
            {
                first = i;
            }
        }
        if (feed_list[first] == 0) first = -1;
        return first;
    }

    private string feedback_title, feedback_contents;
    public void feedBackUpdate()
    {
        int feedback_list = feedbackTitle();
        switch (feedback_list)
        {
            case -1:
                feedback_title = "";
                feedback_contents = "";
                break;
            case 0:
                feedback_title = "최우선 개선 사항: 부주의";
                feedback_contents = "발생한 모든 이벤트들의 내용을 종합해본 결과 운전자님께서는 운전 중 부주의한 경우가 상당히 잦은 것으로 보입니다. 운전 중 부주의는 교통사고 발생 원인의 1위를 차지할 만큼 반드시 해결하셔야 할 문제입니다. 운전 시에는 운전에만 집중할 수 있도록 노력하셔야 하고, 집중할 수 없다면 운전을 하지 않으시는 것이 좋습니다.\n";
                break;
            case 1:
                feedback_title = "최우선 개선 사항: 운전 미숙";
                feedback_contents = "발생한 모든 이벤트들의 내용을 종합해본 결과 운전자님께서는 아직 전반적으로 핸들 조작 등 운전 자체에 조금 미숙하신것으로 보입니다. 이는 연습으로 충분히 극복할 수 있으므로 자신감을 가지고 계속 노력하시면 개선될 것입니다.\n";
                break;
            case 2:
                feedback_title = "최우선 개선 사항: 감속";
                feedback_contents = "발생한 모든 이벤트들의 내용을 종합해본 결과 운전자님께서는 너무 급하게 운전을 하시는 것 같습니다. 운전은 언제든 예측할 수 없는 상황이 발생할 수 있으므로 감속과 주변 상황 판단은 운전에 있어 매우 중요한 요소입니다. 항상 속도보다는 안전을 우선하여 운전할 수 있도록 신경써 주시길 바랍니다.\n";
                break;
            case 3:
                feedback_title = "최우선 개선 사항: 안전거리 확보";
                feedback_contents = "발생한 모든 이벤트들의 내용을 종합해본 결과 운전자님께서는 지나치게 다른 차량에 붙거나 또는 지나치게 다른 차량이 붙도록 하시는 것 같습니다. 운전자님의 속도에 맞춰 적절한 안전거리를 계산하고, 이를 감각적으로 유지할 수 있어야 합니다. 또한 운전자님의 후방에 위치한 차량도 운전자님과 안전거리를 유지할 수 있도록 너무 느리지도 빠르지도 않는 적정 속도를 유지하는 것도 중요합니다.\n";
                break;
        }
        Feedback_Title.text = feedback_title;
        Feedback_Contents.text = feedback_contents;
        if (RedLightOverCount * 3 + RedLightSlowCount > 5)
        {
            if (feedback_title == "") feedback_title = "최우선 개선 사항: 신호위반";
            feedback_contents += "운전자님께서는 신호에 대한 대응이 미흡한 것으로 판단됩니다. 시야를 넓게 가지고 다음 신호의 위치와 상황을 판단할 수 있는 습관을 기르셔야 합니다. 또한 신호가 주황색 불인 경우 속도를 줄이고 다음 신호를 기다리는 습관을 기르시는 것이 좋습니다.";
        }
        /*
        if (crashCount > 5)
        {
            Feedback_Control.text = "차량 조작이 미숙한 것으로 판단됩니다. 특히 핸들 조작 능력과 차량의 위치 파악 능력이 부족한 것으로 의심되므로 운전 시 해당 부분을 중점적으로 더 고민하시는 것이 좋을 것 같습니다.";
        }
        */
    }
}
