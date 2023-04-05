using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEvents : MonoBehaviour
{
    public static InputEvents instance;

    public string defaultActionMap = "Web";
    
    public InputAsset                                          inputAsset;
    public Dictionary<string, InputActionMap>                  actionMaps;
    public Dictionary<string, Dictionary<string, InputAction>> inputActions;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
        __init();
    }

    public void __init()
    {
        inputAsset   = new InputAsset();
        actionMaps   = new Dictionary<string, InputActionMap>();
        inputActions = new Dictionary<string, Dictionary<string, InputAction>>();

        IEnumerator<InputAction> x = inputAsset.GetEnumerator();
        while (x.MoveNext())
        {
            if (!inputActions.ContainsKey(x.Current.actionMap.name))
            {
                actionMaps.Add(x.Current.actionMap.name, x.Current.actionMap);
                inputActions.Add(x.Current.actionMap.name, new Dictionary<string, InputAction>());
            }

            inputActions[x.Current.actionMap.name].Add(x.Current.name, x.Current);
        }
        ToggleActionMap(defaultActionMap, true);
    }

    public void ToggleActionMap(string actionMapName, bool state)
    {
        if (inputActions.ContainsKey(actionMapName))
        {
            if (state)
            {
                actionMaps[actionMapName].Enable();
            }
            else actionMaps[actionMapName].Disable();
        }
    }
}
