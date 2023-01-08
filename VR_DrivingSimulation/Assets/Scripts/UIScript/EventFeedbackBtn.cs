using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EventFeedbackBtn : MonoBehaviour
{
    public TMP_Text btn_title;
    public TMP_Text feedback_title;
    public TMP_Text event_feed_contents;
    private string title;
    private string feedback;


    public void BtnTxtUpdate(string eventTitle, string eventFeedCont)
    {
        btn_title.text = eventTitle;
        title = eventTitle;
        feedback = eventFeedCont;
    }


    public void FeedbackUpdate()
    {
        feedback_title.text = title;
        event_feed_contents.text = feedback;
    }

}
