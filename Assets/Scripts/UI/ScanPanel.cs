using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanPanel : MonoBehaviour
{
    public Image ScanCircle;
    public Text NameText;
    public Text InfoText;
    private void Start()
    {
        EventManager.Instance.AddValueChangeEvent("OnScanValueChange", SetScanCircle);
        EventManager.Instance.AddReferenceEvents("OnScanFinish", ShowFishInfo);
    }
    void SetScanCircle(float value)
    {
        ScanCircle.fillAmount = value;
    }
    void ShowFishInfo(object fishData)
    {
        FishData data =fishData  as FishData;
        NameText.text = data.briefInfo;
        InfoText.text = data.info;
    }
}
