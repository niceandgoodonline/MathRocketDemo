using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchPadClearButton : MonoBehaviour
{
    public delegate void noneDelegate();
    public static event noneDelegate Emit;

    public void TriggerEmit()
    {
        if (Emit != null) Emit();
    }
}
