using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingEvent : MonoBehaviour
{
    [System.Serializable]
    public struct Feedback_List
    {
        [Header("ScoreBoard: Total Score\nEvent: Deduction Point")]
        public int score;
        public int negligent;
        public int poor_driving;
        public int deceleration;
        public int safety_distance;
        public void init()
        {
            score = 0;
            negligent = 0;
            poor_driving = 0;
            deceleration = 0;
            safety_distance = 0;
        }
        public int getNumOfFeed()
        {
            return 4;
        }
    }

    [SerializeField] private Feedback_List EventScore;

    public Feedback_List getEventScore()
    {
        return EventScore;
    }

    public int getScore()
    {
        return EventScore.score;
    }

    [Space(10f)]
    [Header("Event Situation")]
    public string event_title = "사고 상황";
    [Space(5f)]
    [Header("Event Feedback Contents")]
    [TextArea (2, 6)]
    public string event_feedback = "피드백 내용";
}
