using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartPanel : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    private void Start()
    {
        StartButton.onClick.AddListener(NextScene);
        QuitButton.onClick.AddListener(Quit);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void Quit()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

            Application.Quit();

#endif
    }
}
