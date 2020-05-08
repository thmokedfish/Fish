using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanPanel : MonoBehaviour
{
    public Image ScanCircle;
    public Text NameText;
    public Text InfoText;
    public Image task;
    public Image taskFinish;
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
        if (data.ID == 12)
        {
            task.gameObject.SetActive(false);
            taskFinish.gameObject.SetActive(true);
            Invoke("FinishTask", 3.0f);
        }
        InfoText.text = data.ID.ToString();
    }
    void FinishTask()
    {
        taskFinish.gameObject.SetActive(false);
        Destroy(taskFinish);
        Destroy(task);
    }
}
