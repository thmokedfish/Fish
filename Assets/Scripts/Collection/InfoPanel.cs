using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text name;
    public Text rare;
    public Text info;
    public Button button;
    public GameObject MainPanel;
    public GameObject Panel;
    public void SetText(string n, string r, string i)
    {
        name.text = n;
        rare.text = r;
        info.text = i;
        button.onClick.AddListener(Return);
    }
    void Return()
    {
        Panel.SetActive(false);
        MainPanel.SetActive(true);
    }
}
