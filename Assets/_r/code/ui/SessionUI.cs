using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionUI : MonoBehaviour
{
    public Button startButton, submitButton, stopButton;

    public delegate void            noneDelgate();
    public static event noneDelgate SubmitEvent;
    private void __SubmitEvent()
    {
        if (SubmitEvent != null) SubmitEvent();
    }
    
    public delegate void             boolDelegate(bool _b);
    public static event boolDelegate SessionEvent;
    private void __SessionEvent(bool _b)
    {
        if (SessionEvent != null) SessionEvent(_b);
    }
    public void __init()
    {
        
    }

    private void OnEnable()
    {
        __init();
        SubscribeToEvents(true);
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            

        }
        else
        {
            
        }
    }
    
    private void ToggleUIButtons()
    {
        startButton.gameObject.SetActive(!startButton.gameObject.activeSelf);
        submitButton.gameObject.SetActive(!submitButton.gameObject.activeSelf);
        stopButton.gameObject.SetActive(!stopButton.gameObject.activeSelf);
    }
    
    public void SendStartSessionEvent()
    {
        __SessionEvent(true);
        ToggleUIButtons();
    }
    
    public void SendStopSessionEvent()
    {
        __SessionEvent(false);
        ToggleUIButtons();
    }
    
    public void SendSubmitEvent()
    {
        __SubmitEvent();
    }
}
