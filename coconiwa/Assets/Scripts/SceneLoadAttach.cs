using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAttach : MonoBehaviour
{

    [SerializeField]
    string LoadSceneName;

    [SerializeField]
    float transitionTime = 0.0f;

    public void LoadSceneAsync()
    {
        if (transitionTime > 0.0f)
        {
            transform.parent.GetComponent<UnderBerMenu>().ChangeScene(transitionTime, LoadSceneName);
            return;
        }

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

    public void MapSceneLoad(string FileID)
    {
        AppData.SelectTargetName = FileID;
        GameObject.Find("Canvas1").GetComponentInChildren<UnderBerMenu>().ChangeScene(1.0f, "Content");
    }
}
