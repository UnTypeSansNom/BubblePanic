using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class sc_TargetClic : MonoBehaviour
{
    public UnityEvent GetClicked;
    public float travelDuration;

    public void ReceiveOrder()
    {
        GetClicked.Invoke();
    }
}
