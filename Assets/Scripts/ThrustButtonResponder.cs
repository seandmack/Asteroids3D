using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThrustButtonResponder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool touched;
    private int pointerID;
    private bool canThrust;

    void Awake()
    {
        touched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canThrust = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if(data.pointerId == pointerID)
        {
            canThrust = false;
            touched = false;
        }
    }

    public bool CanThrust()
    {
        return canThrust;
    }
}
