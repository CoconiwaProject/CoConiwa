using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAttach : MonoBehaviour
{

    [SerializeField]
    string LoadSceneName;

    [SerializeField]
    bool isTransition = true;

    public void LoadSceneAsync()
    {
        if (isTransition)
        {
            transform.parent.GetComponent<UnderBerMenu>().ChangeScene(LoadSceneName);
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
        GameObject.Find("Canvas2").GetComponentInChildren<UnderBerMenu>().ChangeScene("Content");
    }
}
