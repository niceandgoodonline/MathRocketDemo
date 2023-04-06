using System.Collections;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerDisplay;
    public float    timeThisSession;
    
    private Coroutine      timerCoroutine;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
    
    public void StartTimer(float timeAmount)
    {
        timeThisSession = timeAmount;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerDisplay.text = "DONE!";
    }

    private IEnumerator RunTimer()
    {
        while (timeThisSession > 0)
        {
            timeThisSession   -= 1;
            timerDisplay.text =  timeThisSession.ToString();
            yield return waitOneSecond;
        }
        timerCoroutine = null;
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
            GameSession.StartSession += StartTimer;
            GameSession.StopSession  += StopTimer;
        }
        else
        {
            GameSession.StartSession -= StartTimer;
            GameSession.StopSession  -= StopTimer;   
        }
    }
}
