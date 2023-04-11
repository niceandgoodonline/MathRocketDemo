using System.Collections.Generic;
using UnityEngine;


public struct GenerateQuestionValues
{
    public int  minDigits, maxDigits,   minOperations,  maxOperations;
    public bool addition,  subtraction, multiplication, division, integers, fractions;
    public GenerateQuestionValues(int _minDigits,   int  _maxDigits, int _minOperations, int _maxOperations, bool _addition,
        bool                          _subtraction, bool _multiplication, bool _division, bool _integers, bool _fractions)
    {
        minDigits      = _minDigits;
        maxDigits      = _maxDigits;
        minOperations  = _minOperations;
        maxOperations  = _maxOperations;
        addition       = _addition;
        subtraction    = _subtraction;
        multiplication = _multiplication;
        division       = _division;
        integers       = _integers;
        fractions      = _fractions;
    }
}

public class GenerateQuestions : MonoBehaviour
{
    [Header("Complexity Parameters")]
    public int          minDigits, maxDigits, minOperations, maxOperations;
    public bool         addition, subtraction, multiplication, division, integers, fractions;
    public List<string> currentOperators, exampleOperators;

    [Header("Editor Peek")]
    public string currentQuestion;
    public string currentAnswer;
    
    private         Calculator             calc = new Calculator();
    private         Coroutine              generateQuestionCoroutine;
    private         GenerateQuestionValues currentValues;
    private         GenerateQuestionValues exampleValues;
    public delegate void                   questionDelegate(string question, string answer);
    public static event questionDelegate   NewQuestion;
    private void __NewQuestion(string question, string answer)
    {
        if (NewQuestion != null) NewQuestion(question, answer);
    }

    public static event questionDelegate NewExampleQuestion;
    private void __NewExampleQuestion(string question, string answer)
    {
        if (NewExampleQuestion != null) NewExampleQuestion(question, answer);
    }

    public delegate void valuesDelegate(GenerateQuestionValues _values);
    public static event valuesDelegate EmitValues;
    private void __EmitValues(GenerateQuestionValues _values)
    {
        if (EmitValues != null) EmitValues(_values);
    }
    
    public void __init()
    {
        if (minDigits < 1) minDigits               = 1;
        if (maxDigits < minDigits) maxDigits = minDigits + 1;
        if (minOperations < 2) minOperations                   = 2;
        if (maxOperations < minOperations) maxOperations       = minOperations + 1;

        currentValues = new GenerateQuestionValues(minDigits, maxDigits, minOperations, maxOperations,
            addition, subtraction, multiplication, division, integers, fractions);
        currentOperators = GenerateOperatorList(currentValues);
    }

    private List<string> GenerateOperatorList(GenerateQuestionValues _values)
    {
        List<string> _new = new List<string>();

        if(_values.addition) _new.Add("+");
        if(_values.subtraction) _new.Add("-");
        if(_values.multiplication) _new.Add("*");
        if(_values.division) _new.Add("/");
        
        if (_new.Count < 1)
        {
            _values.addition = true;
            _new.Add("+");
        }

        return _new;
    }

    public bool GenerateQuestion(GenerateQuestionValues _values, List<string> operatorsList)
    {
        int    operations = Random.Range(_values.minOperations, _values.maxOperations);
        string _a         = "";
        currentQuestion   = "";
        for (int i = 0; i < operations; i++)
        {
            if (i == 0) _a = GenerateNumber(Random.Range(_values.minDigits, _values.maxDigits));
            else _a = "";
            
            string _op = GenerateOperator(operatorsList);
            string _b  = GenerateNumber(Random.Range(_values.minDigits, _values.maxDigits));
            currentQuestion += $"{_a} {_op} {_b}";
        }
        float thisAnswer = calc.Calculate(currentQuestion);
        if (!_values.integers && thisAnswer < 1) return false;
        currentAnswer = thisAnswer.ToString();
        if (!_values.fractions && currentAnswer.Contains(".")) return false;
        return true;
    }


    private string GenerateOperator(List<string> operators)
    {
        return operators[Random.Range(0, operators.Count)];
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
        if(GenerateQuestion(currentValues, currentOperators)) __NewQuestion(currentQuestion, currentAnswer);
        else HandleQuestionRequest();
    }
    
    private void HandleGameSettingsEvent(bool ignore)
    {
        __EmitValues(currentValues);
    }

    private void HandleGameSettingChangeEvent(GenerateQuestionValues _values)
    {
        exampleOperators = GenerateOperatorList(_values);
        if(GenerateQuestion(_values, exampleOperators)) __NewExampleQuestion(currentQuestion, currentAnswer);
        else HandleGameSettingChangeEvent(_values);
    }

    private void HandleGameSettingSaveEvent(GenerateQuestionValues _values)
    {
        currentValues = _values;
    }

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            GameSession.NextQuestion        += HandleQuestionRequest;
            GameSettingsButton.Emit         += HandleGameSettingsEvent;
            GameSettingsState.ExampleUpdate += HandleGameSettingChangeEvent;
            GameSettingsState.Save          += HandleGameSettingSaveEvent;
        }
        else
        {
            GameSession.NextQuestion        -= HandleQuestionRequest;
            GameSettingsButton.Emit         -= HandleGameSettingsEvent;
            GameSettingsState.ExampleUpdate -= HandleGameSettingChangeEvent;
            GameSettingsState.Save          -= HandleGameSettingSaveEvent;
        }
    }

}
