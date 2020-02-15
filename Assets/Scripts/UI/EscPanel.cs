using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EscPanel : MonoBehaviour
{
    public GameObject PreferencePanel;
    [Header("Buttons")]
    public Button CollectionSceneButton;
    public Button PreferenceButton;
    public Button StartSceneButton;

    private void Start()
    {
        StartSceneButton.onClick.AddListener(delegate () { SwitchScene(0); });
        PreferenceButton.onClick.AddListener(delegate () { UIManager.Instance.SetActiveUI(PreferencePanel); });
    }

    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
