using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestionUI : MonoBehaviour
{
    public CultureInfo cultureInfo = CultureInfo.InvariantCulture;
    
    public TMP_Text questionDisplay;
    public TMP_Text answerField;
    
    private string      currentActionMap, currentAnswer;
    private InputEvents inputEvents;
    private bool        subscribedToInput = false;
    
    public delegate void noneDelegate();
    public static event noneDelegate CorrectAnswer;
    private void __CorrectAnswer()
    {
        if (CorrectAnswer != null) CorrectAnswer();
    }
    
    public static event noneDelegate IncorrectAnswer;
    private void __IncorrectAnswer()
    {
        if (IncorrectAnswer != null) IncorrectAnswer();
    }
    
    
    public void __init()
    {
        questionDisplay.text = "Questions coming up soon...";
        answerField.text     = "";
        inputEvents          = InputEvents.instance;
        if (inputEvents == null) StartCoroutine(FindInputEvents());
        else SubscribeToEvents(true);
;    }

    private IEnumerator FindInputEvents()
    {
        while (inputEvents == null)
        {
            yield return null;
            inputEvents = InputEvents.instance;
        }
        SubscribeToEvents(true);
    }

    private void OnEnable()
    {
        __init();
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    private void SubscribeToEvents(bool state)
    {
        if (!subscribedToInput)
        {
            subscribedToInput = true;
            currentActionMap  = inputEvents.defaultActionMap;
            ToggleInput(true);
        }
        if(state)
        {
            SessionUI.SubmitEvent         += CompareSubmission;
            GenerateQuestions.NewQuestion += HandleNewQuestionEvent;
        }
        else
        {
            SessionUI.SubmitEvent         += CompareSubmission;
            GenerateQuestions.NewQuestion -= HandleNewQuestionEvent;
        }
    }
    private void HandleNumberEvent(InputAction.CallbackContext cx)
    {
        string controlString = cx.control.ToString();
        string key           = controlString.Substring(controlString.Length - 1);

        if (key == "s")
        {
            if (answerField.text.Length > 0) answerField.text  = (float.Parse(answerField.text, cultureInfo.NumberFormat) * -1).ToString();
            else answerField.text = "-";
        }
        else answerField.text            += key;
    }

    private void CompareSubmission()
    {
        if (answerField.text == currentAnswer)
        {
            __CorrectAnswer();
        }
        else
        {
            __IncorrectAnswer();
        }
    }

    private void HandleSubmitEvent(InputAction.CallbackContext cx)
    {
        CompareSubmission();
    }

    private void HandleClearEvent(InputAction.CallbackContext cx)
    {
        answerField.text = "";
    }

    private void HandleDeleteEvent(InputAction.CallbackContext cx)
    {
        answerField.text = answerField.text.Substring(0, Mathf.Clamp(answerField.text.Length - 1, 0, answerField.text.Length));
    }
    public void HandleNewQuestionEvent(string newQuestion, string newAnswer)
    {
        currentAnswer        = newAnswer;
        questionDisplay.text = newQuestion;
        answerField.text     = "";
    }
    public void ToggleInput(bool state)
    {
        if (state)
        {
            inputEvents.inputActions[currentActionMap]["NumberOne"].performed    += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberTwo"].performed    += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberThree"].performed  += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberFour"].performed   += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberFive"].performed   += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberSix"].performed    += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberSeven"].performed  += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberEight"].performed  += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberNine"].performed   += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberZero"].performed   += HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NegativeSign"].performed += HandleNumberEvent;
            
            inputEvents.inputActions[currentActionMap]["Submit"].performed += HandleSubmitEvent;
            inputEvents.inputActions[currentActionMap]["Clear"].performed  += HandleClearEvent;
            inputEvents.inputActions[currentActionMap]["Delete"].performed += HandleDeleteEvent;
        }
        else
        {
            inputEvents.inputActions[currentActionMap]["NumberOne"].performed    -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberTwo"].performed    -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberThree"].performed  -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberFour"].performed   -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberFive"].performed   -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberSix"].performed    -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberSeven"].performed  -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberEight"].performed  -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberNine"].performed   -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NumberZero"].performed   -= HandleNumberEvent;
            inputEvents.inputActions[currentActionMap]["NegativeSign"].performed -= HandleNumberEvent;
            
            inputEvents.inputActions[currentActionMap]["Submit"].performed -= HandleSubmitEvent;
            inputEvents.inputActions[currentActionMap]["Clear"].performed  -= HandleClearEvent;
            inputEvents.inputActions[currentActionMap]["Delete"].performed -= HandleDeleteEvent;
        }
    }
}
