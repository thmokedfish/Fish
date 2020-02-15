using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject EscPanel;

    private Stack<GameObject> UIPanels = new Stack<GameObject>();
    public static UIManager Instance;


    private void Awake()
    {
        if(Instance==null)
        Instance = this;
    }

    private void Start()
    {
        EventManager.Instance.InvokeValueChangeEvent("CursorHide", 1);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIPanels.Count == 0)
            {
                SetActiveUI(EscPanel);
            }
            else
            {
                GameObject panel = UIPanels.Pop();
                panel.SetActive(false);
            }
            if(UIPanels.Count==0)
            {
                EventManager.Instance.InvokeValueChangeEvent("CursorHide", 1);
            }
            else
            {
                EventManager.Instance.InvokeValueChangeEvent("CursorHide", 0);
            }
        }
    }

    public void SetActiveUI(GameObject go)
    {
        go.SetActive(true);
        UIPanels.Push(go);
    }

}
