using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsButton : MonoBehaviour
{
    public GameObject                settings, save;

    public delegate void             boolDelegate(bool b);
    public static event boolDelegate Emit;

    private void EmitEvent(bool b)
    {
        if (Emit != null) Emit(b);
    }

    public void TriggerEvent()
    {
        settings.SetActive(!settings.activeSelf);
        save.SetActive(!save.activeSelf);
        EmitEvent(settings.activeSelf);
    }
}
