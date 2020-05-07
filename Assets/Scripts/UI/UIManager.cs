using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject EscPanel;
    public Slider slider;

    private Stack<GameObject> UIPanels = new Stack<GameObject>();
    public static UIManager Instance;

    private void Awake()
    {
        if(Instance==null)
        Instance = this;
    }

    private void Start()
    {
        Invoke("HideCursorAtStart", 0.05f);
    }
    private void HideCursorAtStart()
    {
        EventManager.Instance.InvokeValueChangeEvent("CursorHide", 1);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIPanels.Count == 0)
            {
                SetPanelActive(EscPanel);
                EventManager.Instance.InvokeValueChangeEvent("CursorHide", 0);
            }
            else
            {
                PopPanel();
            }
        }
    }

    public void SetPanelActive(GameObject go)
    {
        go.SetActive(true);
        UIPanels.Push(go);
    }

    public void PopPanel()
    {
        GameObject panel = UIPanels.Pop();
        panel.SetActive(false);
        if (UIPanels.Count == 0)
        {
            EventManager.Instance.InvokeValueChangeEvent("CursorHide", 1);
        }
    }
    
}
