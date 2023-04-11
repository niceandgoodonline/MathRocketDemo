using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingSliders : MonoBehaviour
{
    public Slider         minDigitSlider,     maxDigitSlider,     minOperationSlider,     maxOperationSlider;
    public TMP_InputField minDigitInputField, maxDigitInputField, minOperationInputField, maxOperationInputField;

    public void SetValues(GenerateQuestionValues _values)
    {
        minDigitSlider.value     = _values.minDigits;
        maxDigitSlider.value     = _values.maxDigits;
        minOperationSlider.value = _values.minOperations;
        maxOperationSlider.value = _values.maxOperations;
        
        minDigitInputField.text     = _values.minDigits.ToString();
        maxDigitInputField.text     = _values.maxDigits.ToString();
        minOperationInputField.text = _values.minOperations.ToString();
        maxOperationInputField.text = _values.maxOperations.ToString();
    }
}
