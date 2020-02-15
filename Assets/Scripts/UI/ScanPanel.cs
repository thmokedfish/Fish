using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanPanel : MonoBehaviour
{
    public Image ScanCircle;
    private void Start()
    {
        EventManager.Instance.AddValueChangeEvent("OnScanValueChange", SetScanCircle);
        EventManager.Instance.AddReferenceEvents("OnScanFinish", ShowFishInfo);
    }
    void SetScanCircle(float value)
    {
        ScanCircle.fillAmount = value;
    }
    void ShowFishInfo(object fishName)
    {
        string name = fishName as string;
    }
}
