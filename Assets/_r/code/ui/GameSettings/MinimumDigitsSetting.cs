using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinimumDigitsSetting : MonoBehaviour
{
    public Slider         slider;
    public TMP_InputField inputField;
    
    public void __init()
    {
        
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

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            
        }
        else
        {
            
        }
    }
    //void Start() {}
    //void Update() {}
}
