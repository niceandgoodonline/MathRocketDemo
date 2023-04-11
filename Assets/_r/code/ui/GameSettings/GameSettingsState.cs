using System;
using UnityEngine;

public class GameSettingsState : MonoBehaviour
{
    public  GameSettingSliders     sliders;
    public  GameSettingToggles     toggles;
    private GenerateQuestionValues currentValues;
    
    public delegate void generateQuestionValuesDelegate(GenerateQuestionValues _values);
    public static event generateQuestionValuesDelegate Save;
    private void __Save(GenerateQuestionValues _values)
    {
        if (Save != null) Save(_values);
    }

    public static event generateQuestionValuesDelegate ExampleUpdate;
    private void __ExampleUpdate(GenerateQuestionValues _values)
    {
        if (ExampleUpdate != null) ExampleUpdate(_values);
    }

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
        if (sliders != null) SetSliderListeners(state); 
        if (toggles != null) SetToggleListeners(state);
        if(state)
        {
            GenerateQuestions.EmitValues += HandleCurrentValuesEvent;
        }
        else
        {
            GenerateQuestions.EmitValues -= HandleCurrentValuesEvent;
        }
    }

    public void TriggerSave()
    {
        __Save(currentValues);
    }

    private void HandleCurrentValuesEvent(GenerateQuestionValues _values)
    {
        currentValues = _values;
        if (sliders != null) sliders.SetValues(_values);
        if (toggles != null) toggles.SetValues(_values);
    }
    
    private void SetSliderListeners(bool state)
    {
        if (state)
        {
            sliders.minDigitSlider.onValueChanged.AddListener(MinimumDigitSliderValueChange);
            sliders.maxDigitSlider.onValueChanged.AddListener(MaximumDigitSliderValueChange);
            sliders.minOperationSlider.onValueChanged.AddListener(MinimumOperationSliderValueChange);
            sliders.maxOperationSlider.onValueChanged.AddListener(MaximumOperationSliderValueChange);
            sliders.minDigitInputField.onValueChanged.AddListener(MinimumDigitInputFieldValueChange);
            sliders.maxDigitInputField.onValueChanged.AddListener(MaximumDigitInputFieldValueChangeValueChange);
            sliders.minOperationInputField.onValueChanged.AddListener(MinimumOperationInputFieldValueChangeValueChange);
            sliders.maxOperationInputField.onValueChanged.AddListener(MaximumOperationInputFieldValueChangeValueChange);
        }
        else
        {
            sliders.minDigitSlider.onValueChanged.RemoveAllListeners();
            sliders.maxDigitSlider.onValueChanged.RemoveAllListeners();
            sliders.minOperationSlider.onValueChanged.RemoveAllListeners();
            sliders.maxOperationSlider.onValueChanged.RemoveAllListeners();
            sliders.minDigitInputField.onValueChanged.RemoveAllListeners();
            sliders.maxDigitInputField.onValueChanged.RemoveAllListeners();
            sliders.minOperationInputField.onValueChanged.RemoveAllListeners();
            sliders.maxOperationInputField.onValueChanged.RemoveAllListeners();
        }
    }

    private void MinimumDigitSliderValueChange(float f)
    {
        string _f = f.ToString();
        currentValues.minDigits = (int)f;
        if (sliders.minDigitInputField.text != _f) sliders.minDigitInputField.text = _f;
        if (sliders.maxDigitSlider.value < f)
        {
            sliders.maxDigitSlider.value = f;
            if (sliders.maxDigitInputField.text != _f) sliders.maxDigitInputField.text = _f;
        }
        ExampleUpdate(currentValues);
    }
    
    private void MaximumDigitSliderValueChange(float f)
    {
        string _f = f.ToString();
        currentValues.maxDigits = (int)f;
        if (sliders.maxDigitInputField.text != _f) sliders.maxDigitInputField.text = _f;
        if (sliders.minDigitSlider.value > f)
        {
            sliders.minDigitSlider.value = f;
            if (sliders.minDigitInputField.text != _f) sliders.minDigitInputField.text = _f;
        }
        ExampleUpdate(currentValues);
    }
        
    private void MinimumOperationSliderValueChange(float f)
    {
        string _f = f.ToString();
        currentValues.minOperations = (int)f;
        if (sliders.minOperationInputField.text != _f) sliders.minOperationInputField.text = _f;
        if (sliders.maxOperationSlider.value < f)
        {
            sliders.maxOperationSlider.value = f;
            if (sliders.maxOperationInputField.text != _f) sliders.maxOperationInputField.text = _f;
        }
        ExampleUpdate(currentValues);
    }
    
    private void MaximumOperationSliderValueChange(float f)
    {
        string _f = f.ToString();
        currentValues.maxOperations = (int)f;
        if (sliders.maxOperationInputField.text != _f) sliders.maxOperationInputField.text = _f;
        if (sliders.minOperationSlider.value > f)
        {
            sliders.minOperationSlider.value = f;
            if (sliders.minOperationInputField.text != _f) sliders.minOperationInputField.text = _f;
        }
        ExampleUpdate(currentValues);
    }
    
    private void MinimumDigitInputFieldValueChange(string s)
    {
        int n = Int32.Parse(s);
        currentValues.minDigits = n;
        if (sliders.minDigitSlider.value != n) sliders.minDigitSlider.value = n;
        ExampleUpdate(currentValues);
    }

    private void MaximumDigitInputFieldValueChangeValueChange(string s)
    {
        int n = Int32.Parse(s);
        currentValues.maxDigits = n;
        if (sliders.maxDigitSlider.value != n) sliders.maxDigitSlider.value = n;
        ExampleUpdate(currentValues);
    }
    
    private void MinimumOperationInputFieldValueChangeValueChange(string s)
    {
        int n = Int32.Parse(s);
        currentValues.minOperations = n;
        if (sliders.minOperationSlider.value != n) sliders.minOperationSlider.value = n;
        ExampleUpdate(currentValues);
    }
        
    private void MaximumOperationInputFieldValueChangeValueChange(string s)
    {
        int n = Int32.Parse(s);
        currentValues.maxOperations = n;
        if (sliders.maxOperationSlider.value != n) sliders.maxOperationSlider.value = n;
        ExampleUpdate(currentValues);
    }
    
    private void SetToggleListeners(bool state)
    {
        if (state)
        {
            toggles.addition.onValueChanged.AddListener(AdditionToggleValueChange);
            toggles.subtraction.onValueChanged.AddListener(SubtractionToggleValueChange);
            toggles.multiplication.onValueChanged.AddListener(MultiplicationToggleValueChange);
            toggles.division.onValueChanged.AddListener(DivisionToggleValueChange);
            toggles.integers.onValueChanged.AddListener(IntegerToggleValueChange);
            toggles.fractions.onValueChanged.AddListener(FractionToggleValueChange);
        }
        else
        {
            toggles.addition.onValueChanged.RemoveAllListeners();
            toggles.subtraction.onValueChanged.RemoveAllListeners();
            toggles.multiplication.onValueChanged.RemoveAllListeners();
            toggles.division.onValueChanged.RemoveAllListeners();
            toggles.integers.onValueChanged.RemoveAllListeners();
            toggles.fractions.onValueChanged.RemoveAllListeners();
        }
    }

    private void AdditionToggleValueChange(bool state)
    {
        currentValues.addition = state;
    }
    
    private void SubtractionToggleValueChange(bool state)
    {
        currentValues.subtraction = state;
    }
    
    private void MultiplicationToggleValueChange(bool state)
    {
        currentValues.multiplication = state;
    }
    
    private void DivisionToggleValueChange(bool state)
    {
        currentValues.division = state;
    }
    
    private void IntegerToggleValueChange(bool state)
    {
        currentValues.integers = state;
    }
    
    private void FractionToggleValueChange(bool state)
    {
        currentValues.fractions = state;
    }
}
