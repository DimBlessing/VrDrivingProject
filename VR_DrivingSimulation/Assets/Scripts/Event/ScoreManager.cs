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
    public int init_score = 100;//���� ����(������ 100)
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
        if (ScoreBoard.score >= 75) ResultText.text = "�հ��Դϴ�!";
        else ResultText.text = "���հ��Դϴ�.";
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
            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("���� �ӵ� ���� / -" + speedingCnt +"��", "���� �ӵ��� �ʰ��ϼ̽��ϴ�.");
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
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / -" + eventScore.ToString() + "��", other.GetComponent<DrivingEvent>().event_feedback);
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
                            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("�ɰ��� �浹 ��� �߻� / �ǰ�", "�ٸ� ���� �Ǵ� �̵� ��ġ�� ū ����� �߻��� �����Ǿ� ��� �ǰ�ó�� �Ǿ����ϴ�.\n" + other.GetComponent<DrivingEvent>().event_feedback);
                            EventList.GetComponent<EventBtnActive>().event_btn_active();
                            setFeedList(other.GetComponent<DrivingEvent>().getEventScore());
                            Invoke("CrashSetScoreZero", 2.0f);
                        }
                        else
                        {
                            eventScore = other.GetComponent<DrivingEvent>().getScore();
                            if (ScoreBoard.score < eventScore) eventScore = ScoreBoard.score;
                            EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / -" + eventScore.ToString() + "��", other.GetComponent<DrivingEvent>().event_feedback);
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
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("������ �浹 ��� �߻� / �ǰ�", "������� �浹�� �����Ǿ� ��� �ǰ�ó�� �Ǿ����ϴ�.\n�������� ��� ���۽����� ���ηζپ�� �� �ְ�, �������� �� ������ �ʱ⿡ ���� �߿��� �׻� �ֺ� �����ڵ��� �ൿ�� ���Ǹ� ��￩�� �մϴ�.");
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
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate(other.GetComponent<DrivingEvent>().event_title + " / �ǰ�", other.GetComponent<DrivingEvent>().event_feedback);
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
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("��ȣ ���� / -15��", "��ȣ�� �����ϼ̽��ϴ�.\n�׻� ���� ��ȣ ���¸� �ֽ��Ͻð�, �ʷ� ��ȣ�� �ٲ��� ���� �Ǿ��ٸ� ��ȣ ���濡 ���ؼ��� ����ϼž� �մϴ�.\n�����ο����� �ֺ� ������ ��ȣ�� ���¸� ���� ���� ������ ��ȣ ��ȭ�� �����ϴ� �͵� ���� ����Դϴ�.");
                    EventList.GetComponent<EventBtnActive>().event_btn_active();
                    ScoreBoard.score -= 15;
                    RedLightOverCount += 1;
                    Destroy(other);
                    break;
                case "RedLightSlow":
                    if (PlayerCar.velocity.sqrMagnitude > 25f)
                    {
                        EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("���� ���� ���� / -5��", "��ȣ�� ���������� ���� ���� Ȯ���ߴٸ� ����� �Ÿ��� �ΰ� ������ �����ؾ� �մϴ�.\nƯ�� Ⱦ�ܺ��� ��ó������ ������ ���߻�Ȳ�� �߻��� �� �����Ƿ� �� ��ȣ�� �ֺ� ��Ȳ�� �����Ͽ� ������ �� �־�� �մϴ�.");
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
                    EventList.GetComponent<EventBtnActive>().buttons[EventList.GetComponent<EventBtnActive>().next_active].GetComponent<EventFeedbackBtn>().BtnTxtUpdate("ȸ�� ���� ���� / -10��", "ȸ�� ���� ���� ������ ��� ���� �� ���� ��Ȳ�� ���캸�ð� �ٸ� ������ Ȯ���Ͻ� �Ŀ� �����ϰ� �����Ͻô� ���� ����帳�ϴ�.");
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
                feedback_title = "�ֿ켱 ���� ����: ������";
                feedback_contents = "�߻��� ��� �̺�Ʈ���� ������ �����غ� ��� �����ڴԲ����� ���� �� �������� ��찡 ����� ���� ������ ���Դϴ�. ���� �� �����Ǵ� ������ �߻� ������ 1���� ������ ��ŭ �ݵ�� �ذ��ϼž� �� �����Դϴ�. ���� �ÿ��� �������� ������ �� �ֵ��� ����ϼž� �ϰ�, ������ �� ���ٸ� ������ ���� �����ô� ���� �����ϴ�.\n";
                break;
            case 1:
                feedback_title = "�ֿ켱 ���� ����: ���� �̼�";
                feedback_contents = "�߻��� ��� �̺�Ʈ���� ������ �����غ� ��� �����ڴԲ����� ���� ���������� �ڵ� ���� �� ���� ��ü�� ���� �̼��ϽŰ����� ���Դϴ�. �̴� �������� ����� �غ��� �� �����Ƿ� �ڽŰ��� ������ ��� ����Ͻø� ������ ���Դϴ�.\n";
                break;
            case 2:
                feedback_title = "�ֿ켱 ���� ����: ����";
                feedback_contents = "�߻��� ��� �̺�Ʈ���� ������ �����غ� ��� �����ڴԲ����� �ʹ� ���ϰ� ������ �Ͻô� �� �����ϴ�. ������ ������ ������ �� ���� ��Ȳ�� �߻��� �� �����Ƿ� ���Ӱ� �ֺ� ��Ȳ �Ǵ��� ������ �־� �ſ� �߿��� ����Դϴ�. �׻� �ӵ����ٴ� ������ �켱�Ͽ� ������ �� �ֵ��� �Ű�� �ֽñ� �ٶ��ϴ�.\n";
                break;
            case 3:
                feedback_title = "�ֿ켱 ���� ����: �����Ÿ� Ȯ��";
                feedback_contents = "�߻��� ��� �̺�Ʈ���� ������ �����غ� ��� �����ڴԲ����� ����ġ�� �ٸ� ������ �ٰų� �Ǵ� ����ġ�� �ٸ� ������ �ٵ��� �Ͻô� �� �����ϴ�. �����ڴ��� �ӵ��� ���� ������ �����Ÿ��� ����ϰ�, �̸� ���������� ������ �� �־�� �մϴ�. ���� �����ڴ��� �Ĺ濡 ��ġ�� ������ �����ڴ԰� �����Ÿ��� ������ �� �ֵ��� �ʹ� �������� �������� �ʴ� ���� �ӵ��� �����ϴ� �͵� �߿��մϴ�.\n";
                break;
        }
        Feedback_Title.text = feedback_title;
        Feedback_Contents.text = feedback_contents;
        if (RedLightOverCount * 3 + RedLightSlowCount > 5)
        {
            if (feedback_title == "") feedback_title = "�ֿ켱 ���� ����: ��ȣ����";
            feedback_contents += "�����ڴԲ����� ��ȣ�� ���� ������ ������ ������ �Ǵܵ˴ϴ�. �þ߸� �а� ������ ���� ��ȣ�� ��ġ�� ��Ȳ�� �Ǵ��� �� �ִ� ������ �⸣�ž� �մϴ�. ���� ��ȣ�� ��Ȳ�� ���� ��� �ӵ��� ���̰� ���� ��ȣ�� ��ٸ��� ������ �⸣�ô� ���� �����ϴ�.";
        }
        /*
        if (crashCount > 5)
        {
            Feedback_Control.text = "���� ������ �̼��� ������ �Ǵܵ˴ϴ�. Ư�� �ڵ� ���� �ɷ°� ������ ��ġ �ľ� �ɷ��� ������ ������ �ǽɵǹǷ� ���� �� �ش� �κ��� ���������� �� ����Ͻô� ���� ���� �� �����ϴ�.";
        }
        */
    }
}
