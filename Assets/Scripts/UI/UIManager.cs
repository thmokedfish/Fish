using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image ScanCircle;
    void Start()
    {
        EventManager.Instance.ValueChangeEvents.Add("OnScanValueChange", SetScanCircle);
    }

    void Update()
    {
        
    }

    void SetScanCircle(float value)
    {
        ScanCircle.fillAmount = value;
    }
}
