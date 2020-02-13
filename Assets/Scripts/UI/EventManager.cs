using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnValueChange(float value);
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Dictionary<string, OnValueChange> ValueChangeEvents = new Dictionary<string, OnValueChange>();
    
}
