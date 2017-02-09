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
        FadeIn(() =>
        {
            try
            {
                SceneManager.LoadSceneAsync(LoadSceneName);
            }
            catch
            {
                Debug.Log("LoadSceneNotFound");
            }
        });
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
        SceneManager.LoadSceneAsync("Content");
    }

    void FadeIn(Action action)
    {
        if (transitionTime <= 0)
        {
            action.Invoke();
            return;
        }

        StartCoroutine(transform.parent.GetComponent<UnderBerMenu>().FadeIn(transitionTime, action));
    }
}
