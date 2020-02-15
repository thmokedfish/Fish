using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreferencePanel : MonoBehaviour
{
    public Slider Sensitivity;

    private void Start()
    {
        Sensitivity.onValueChanged.AddListener(delegate (float val) { EventManager.Instance.InvokeValueChangeEvent("Sensitivity", val); });
    }

}
