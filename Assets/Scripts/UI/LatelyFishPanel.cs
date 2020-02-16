using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LatelyFishPanel : MonoBehaviour
{
    public Button[] InfoButtons;
    public GameObject InfoPanel;
    public Text infoText;

    private Text[] ButtonTexts;
    Queue<FishData> CapturedData;
    private FishData[] CapturedArray;
    private bool Inited;
    private void Init()
    {
        CapturedData = DataManager.Instance.LatestCaptured;

        ButtonTexts = new Text[InfoButtons.Length];
        CapturedArray = new FishData[InfoButtons.Length];
        for(int i=0;i< InfoButtons.Length;i++)
        {
           // Debug.Log(i);
           // InfoButtons[i].onClick.AddListener(delegate () 
         //   { 
             //   FishInfoButton_OnClick(i);
         //   });
            ButtonTexts[i] = InfoButtons[i].transform.GetChild(0).GetComponent<Text>();
        }
    }
    private void OnEnable()
    {
        if(!Inited)
        {
            Init();
            Inited = true;
        }
        int count = Mathf.Min(CapturedData.Count, InfoButtons.Length);

        int i =count-1;
        foreach(FishData data in CapturedData)
        {
            if (i <0) { break; }
            CapturedArray[i] = data;
            Debug.Log(i);
            i--;
        }

        for(i=0;i< count;i++)
        {
            ButtonTexts[i].text = CapturedArray[i].briefInfo;
            InfoButtons[i].gameObject.SetActive(true);
        }
    }
    public void FishInfoButton_OnClick(int buttonIndex)
    {
        Debug.Log(buttonIndex);
        UIManager.Instance.SetPanelActive(InfoPanel);
        infoText.text = CapturedArray[buttonIndex].info;
    }


}
