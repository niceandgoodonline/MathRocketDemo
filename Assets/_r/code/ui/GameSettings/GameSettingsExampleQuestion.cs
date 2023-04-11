using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSettingsExampleQuestion : MonoBehaviour
{
    public TMP_Text exampleQuestion, exampleAnswer;

    private void OnEnable()
    {
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
            GenerateQuestions.NewExampleQuestion += HandleNewExampleQuestion;
        }
        else
        {
            GenerateQuestions.NewExampleQuestion -= HandleNewExampleQuestion;
        }
    }

    private void HandleNewExampleQuestion(string question, string answer)
    {
        exampleQuestion.text = question;
        exampleAnswer.text   = answer;
    }
}
