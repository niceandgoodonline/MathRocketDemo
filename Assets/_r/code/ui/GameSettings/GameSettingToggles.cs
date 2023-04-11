using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingToggles : MonoBehaviour
{
    public Toggle addition, subtraction, multiplication, division, integers, fractions;
    
    public void SetValues(GenerateQuestionValues _values)
    {
        Debug.Log("toggles getting set?");
        addition.isOn       = _values.addition;
        subtraction.isOn    = _values.subtraction;
        multiplication.isOn = _values.multiplication; 
        division.isOn       = _values.division; 
        integers.isOn       = _values.integers;
        fractions.isOn      = _values.fractions;
    }
}
