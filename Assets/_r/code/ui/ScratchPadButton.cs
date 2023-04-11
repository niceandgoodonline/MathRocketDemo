using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchPadButton : MonoBehaviour
{
    public GameObject                settings;

    public delegate void             boolDelegate(bool b);
    public static event boolDelegate Emit;

    private void EmitEvent(bool b)
    {
        if (Emit != null) Emit(b);
    }

    public void TriggerEvent()
    {
        settings.SetActive(!settings.activeSelf);
        EmitEvent(settings.activeSelf);
    }
}
