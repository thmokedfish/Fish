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
    public Text target;
    public Text targetDepth;
    public Image info;
    public Text fishName;
    int targetID; 
    private void Start()
    {
        EventManager.Instance.AddReferenceEvents("FirstlyScaned", ShowFirstCapture);
        EventManager.Instance.AddValueChangeEvent("OnScanValueChange", SetScanCircle);
        EventManager.Instance.AddReferenceEvents("OnScanFinish", ShowFishInfo);
        RefreshTarget();
        Invoke("AnimaFalse", 2f);
    }
    void SetScanCircle(float value)
    {
        ScanCircle.fillAmount = value;
    }
    void ShowFishInfo(object fishData)
    {
        FishData data =fishData  as FishData;
        NameText.text = data.briefInfo;
        if (data.ID == targetID)
        {
            task.gameObject.SetActive(false);
            taskFinish.gameObject.SetActive(true);
            Invoke("FinishTask", 3.0f);
            RefreshTarget();
        }
        InfoText.text = data.info;
    }

    void ShowFirstCapture(object Fishdata)
    {
        FishData data = Fishdata as FishData;
        fishName.text = data.briefInfo;
        info.gameObject.SetActive(true);
        Invoke("InfoOver", 2.0f);
    }
    void FinishTask()
    {
        taskFinish.gameObject.SetActive(false);
        task.GetComponent<Animator>().enabled = true;
        task.gameObject.SetActive(true);
        Invoke("AnimaFalse", 2f);
    }
    void RefreshTarget()
    {
        int i=Random.Range(0, DataManager.Instance.LoadedData.datalist.Length);
        target.text = DataManager.Instance.LoadedData.datalist[i].briefInfo;
        targetID = DataManager.Instance.LoadedData.datalist[i].ID;
        targetDepth.text = (DataManager.Instance.LoadedData.datalist[i].depth+1).ToString();
    }
    void AnimaFalse()
    {
        task.GetComponent<Animator>().enabled = false;
    }
    void InfoOver()
    {
        info.gameObject.SetActive(false);
    }
}
