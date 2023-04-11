using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ScratchPad : MonoBehaviour
{
    public LinePool  linePool;
    public LayerMask writableLayer;

    public  bool        scratchPadActive, drawing, newLine;
    private string      currentDrawingDevice;
    private Camera      cam;
    private Line        activeLine;
    private bool        subscribedToInput;
    private InputEvents inputEvents;
    
    private Vector3    worldPoint;
    private RaycastHit hit;
    
    public void __init()
    {
        worldPoint       = new Vector3();
        scratchPadActive = false;
        drawing = false;
        newLine = true;
        if (cam == null) cam = Camera.main;
        subscribedToInput = false;
        inputEvents       = InputEvents.instance;
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
            currentDrawingDevice = $"{inputEvents.currentDrawingDevice}Drawing";
            inputEvents.ToggleActionMap(currentDrawingDevice, true);
            ToggleInput(true);
        }

        if (state)
        {
            ScratchPadButton.Emit      += ToggleScratchPad;
            ScratchPadClearButton.Emit += ClearScratchPad;
        }
        else
        {
            ScratchPadButton.Emit      -= ToggleScratchPad;
            ScratchPadClearButton.Emit -= ClearScratchPad;

        }
    }

    public void ToggleInput(bool state)
    {
        if (state)
        {
            inputEvents.actionMaps[currentDrawingDevice]["DrawLine"].canceled  += HandleDrawLineEvent;
            inputEvents.actionMaps[currentDrawingDevice]["DrawLine"].performed += HandleDrawLineEvent;
        }
        else
        {
            inputEvents.actionMaps[currentDrawingDevice]["DrawLine"].canceled  -= HandleDrawLineEvent;
            inputEvents.actionMaps[currentDrawingDevice]["DrawLine"].performed -= HandleDrawLineEvent;

        }
    }

    private void ToggleScratchPad(bool state)
    {
        scratchPadActive = state;
    }

    private void ClearScratchPad()
    {
        foreach (Transform child in transform)
        {
            linePool.pool.Release(child.gameObject);
        }
    }

    public void HandleDrawLineEvent(InputAction.CallbackContext cx)
    {
        if (cx.performed && scratchPadActive)
        {
            drawing = true;
        }

        if (cx.canceled)
        {
            activeLine = null;
            newLine    = true;
            drawing    = false;
        }
    }

    private void NewLine()
    {
        newLine = false;
        GameObject thisLine = linePool.pool.Get();
        thisLine.transform.parent = transform;
        activeLine                = thisLine.GetComponent<Line>();
    }

    void Update()
    {
        if (scratchPadActive && drawing)
        {
            if (ClickToWorldspace(out hit))
            {
                if (hit.collider.gameObject.CompareTag("Writable"))
                {
                    if (newLine) NewLine();
                    worldPoint.Set(hit.point.x, hit.point.y, hit.collider.transform.position.z);
                    activeLine.UpdateLine(worldPoint);
                }
            }
        }
    }
    
    private bool ClickToWorldspace(out RaycastHit _hit)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray.origin, ray.direction, 
                            out _hit, Mathf.Infinity, 
                            writableLayer, QueryTriggerInteraction.Collide)) return true;
        else return false;
    }
}
