using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnValueChange(float value);

public delegate void ReferenceEvent(object obj);

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private Dictionary<string, OnValueChange> ValueChangeEvents;

    private Dictionary<string, ReferenceEvent> ReferenceEvents;
    private void Awake()
    {
        Instance = this;
        ValueChangeEvents = new Dictionary<string, OnValueChange>();
        ReferenceEvents = new Dictionary<string, ReferenceEvent>();

    }
    //约定:传入的参数在0-1之间
    public void AddValueChangeEvent(string key,OnValueChange val)
    {
      if  (!ValueChangeEvents.ContainsKey(key))
        {
            ValueChangeEvents.Add(key, null);
        }
        ValueChangeEvents[key] += val;
    }

    public void AddReferenceEvents(string key, ReferenceEvent val)
    {
        if (!ReferenceEvents.ContainsKey(key))
        {
            ReferenceEvents.Add(key, null);
        }
        ReferenceEvents[key] += val;
    }

    public void InvokeValueChangeEvent(string key, float val)
    {
        if (ValueChangeEvents.ContainsKey(key))
        {
            ValueChangeEvents[key].Invoke(val);
        }
    }

    public void InvokeReferenceEvents(string key, object val)
    {
        if (ReferenceEvents.ContainsKey(key))
        {
            ReferenceEvents[key].Invoke(val);
        }
    }
}
