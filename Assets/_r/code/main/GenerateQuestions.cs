using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateQuestions : MonoBehaviour
{
    [Header("Complexity Parameters")]
    public int          minNumberDigits, maxNumberDigits, minOperations, maxOperations;
    public List<string> mathOperators;
    public bool         allowNegatives;
    public bool         allowFactions;

    [Header("Editor Peek")]
    public string currentQuestion;
    public string currentAnswer;
    
    private Calculator calc = new Calculator();
    private Coroutine  generateQuestionCoroutine;

    public delegate void questionDelegate(string question, string answer);
    public static event questionDelegate NewQuestion;
    public void __NewQuestion(string question, string answer)
    {
        if (NewQuestion != null)
        {
            NewQuestion(question, answer);
        }
    }
    
    public void __init()
    {
        if (minNumberDigits < 1) minNumberDigits               = 1;
        if (maxNumberDigits < minNumberDigits) maxNumberDigits = minNumberDigits + 1;
        if (minOperations < 2) minOperations                   = 2;
        if (maxOperations < minOperations) maxOperations       = minOperations + 1;
        mathOperators = new List<string>() { "+", "-", "*" };
    }

    public bool GenerateQuestion()
    {
       int    operations = Random.Range(minOperations, maxOperations);
        string _a         = "";
        currentQuestion   = "";
        for (int i = 0; i < operations; i++)
        {
            if (i == 0) _a = GenerateNumber(Random.Range(minNumberDigits, maxNumberDigits));
            else _a = "";
            
            string _op = GenerateOperator();
            string _b  = GenerateNumber(Random.Range(minNumberDigits, maxNumberDigits));
            currentQuestion += $"{_a} {_op} {_b}";
        }
        float thisAnswer = calc.Calculate(currentQuestion);
        if (!allowNegatives && thisAnswer < 1) return false;
        currentAnswer = thisAnswer.ToString();
        if (!allowFactions && currentAnswer.Contains(".")) return false;
        return true;
    }


    private string GenerateOperator()
    {
        return mathOperators[Random.Range(0, mathOperators.Count)];
    }

    private string GenerateNumber(int digits)
    {
        string n = "";
        for (int i = 0; i < digits; i++)
        {
            if (i == 0) n += $"{Random.Range(1, 10)}";
            else n += $"{Random.Range(0, 10)}";
        }

        return n;
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

    private void HandleQuestionRequest()
    {
        if(GenerateQuestion()) __NewQuestion(currentQuestion, currentAnswer);
        else HandleQuestionRequest();
    }

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            GameSession.NextQuestion += HandleQuestionRequest;
        }
        else
        {
            GameSession.NextQuestion -= HandleQuestionRequest;
        }
    }
}
