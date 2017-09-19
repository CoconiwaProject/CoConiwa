using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAttach : MonoBehaviour
{
    [SerializeField]
    string LoadSceneName = "";

    [SerializeField]
    bool isTransition = true;

    public void LoadSceneAsync()
    {
        if (isTransition)
            UnderBerMenu.I.ChangeScene(LoadSceneName);
        else
            SceneLoadManager.I.LoadSceneAsync(LoadSceneName);
    }

    public void LoadScene()
    {
        SceneLoadManager.I.LoadScene(LoadSceneName);
    }
}
