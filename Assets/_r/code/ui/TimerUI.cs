using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerDisplay;
    public int      timerStart;
    
    private Coroutine      timerCoroutine;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
    
    public void StartTimer()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        yield return waitOneSecond;
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
    //void Start() {}
    //void Update() {}
}
