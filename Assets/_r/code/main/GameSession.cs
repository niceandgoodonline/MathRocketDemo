using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int score;
    public int streak;
    
    public  float          sessionSeconds;
    private Coroutine      gameSessionCoroutine;
    private WaitForSeconds waitOneSecond = new WaitForSeconds(1f);

    public delegate void             noneDelegate();
    public delegate void             floatDelegate(float f);
    public delegate void             scoreDelegate(int score, int streak);
    public static event noneDelegate NextQuestion;
    private void __NextQuestion()
    {
        if (NextQuestion != null) NextQuestion();
    }

    public static event noneDelegate StopSession;

    private void __StopSession()
    {
        if (StopSession != null) StopSession();
    }

    public static event floatDelegate StartSession;
    private void __StartSession(float f)
    {
        if (StartSession != null) StartSession(f);
    }
    
    public static event scoreDelegate UpdateScore;
    private void __UpdateScore(int score, int streak)
    {
        if (UpdateScore != null) UpdateScore(score, streak);
    }

    public void __init()
    {
        if (gameSessionCoroutine != null) StopCoroutine(gameSessionCoroutine);
        if (sessionSeconds < 1) sessionSeconds = 1;
        score  = 0;
        streak = 0;
    }

    private IEnumerator TimedGameLoop()
    {
        __NextQuestion();
        while (sessionSeconds > 0f)
        {
            yield return waitOneSecond;
            sessionSeconds -= 1f;
        }
        __StopSession();
        gameSessionCoroutine = null;
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
            SessionUI.SessionEvent     += HandleSessionEvent;
            QuestionUI.CorrectAnswer   += HandleCorrectAnswer;
            QuestionUI.IncorrectAnswer += HandleIncorrectAnswer;

        }
        else
        {
            SessionUI.SessionEvent     -= HandleSessionEvent;
            QuestionUI.CorrectAnswer   -= HandleCorrectAnswer;
            QuestionUI.IncorrectAnswer -= HandleIncorrectAnswer;
        }
    }

    private void HandleSessionEvent(bool state)
    {
        if (state)
        {
            __StartSession(sessionSeconds);
            gameSessionCoroutine = StartCoroutine(TimedGameLoop());
        }
        else
        {
            __StopSession();
            if (gameSessionCoroutine != null) StopCoroutine(gameSessionCoroutine);
        }
    }

    private void HandleCorrectAnswer()
    {
        streak += 1;
        score  += 1 * streak;
        __UpdateScore(score, streak);
        __NextQuestion();
    }
    
    private void HandleIncorrectAnswer()
    {
        streak = 0;
        __UpdateScore(score, streak);
    }
}
