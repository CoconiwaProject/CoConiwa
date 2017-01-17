using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAttach : MonoBehaviour {

    [SerializeField]
    string LoadSceneName;

    public void LoadSceneAsync()
    {
        try
        {
            SceneManager.LoadSceneAsync(LoadSceneName);
        }
        catch
        {
            Debug.Log("LoadSceneNotFound");
        }
    }

    public void LoadScene()
    {
        try
        {
            SceneManager.LoadScene(LoadSceneName);
        }
        catch
        {
            Debug.Log("LoadSceneNotFound");
        }
    }
}
