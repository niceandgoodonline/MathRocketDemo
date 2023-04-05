using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreDisplay, streakDisplay;
    public void __init()
    {
        UpdateScore(0);
        UpdateStreak(0);
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
            GameSession.UpdateScore += HandleUpdateScoreEvent;

        }
        else
        {
            GameSession.UpdateScore -= HandleUpdateScoreEvent;
        }
    }

    private void HandleUpdateScoreEvent(int score, int streak)
    {
        UpdateScore(score);
        UpdateStreak(streak);
    }

    private void UpdateScore(int newScore)
    {
        scoreDisplay.text  = newScore.ToString();
    }

    private void UpdateStreak(int newStreak)
    {
        streakDisplay.text = newStreak.ToString();
    }
}
