using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnderBerMenu : MonoBehaviour
{
    public static UnderBerMenu I;

    List<Button> m_Icons = new List<Button>();

    [SerializeField]
    Image fade = null;

    Coroutine fadeCoroutine;
    Coroutine transitionCoroutine;

    float m_TransitionTime = 0.3f;

    // Use this for initialization
    void Start()
    {
        if(I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;

        DontDestroyOnLoad(transform.parent.gameObject);
        m_Icons.AddRange(transform.GetComponentsInChildren<Button>());
        StartCoroutine(FadeOut(2.0f));
    }

    private void OnLevelWasLoaded(int level)
    {
        Canvas canvas = transform.parent.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 90.0f;

        //読み込み終わったらフェードアウトする
        fadeCoroutine = StartCoroutine(FadeOut(m_TransitionTime));
    }

    public void ChangeIconActive(string nextSceneName)
    {
        //カメラ起動画面に飛んだ場合書き換える。　めんどくさいんで許してちょ
        if(nextSceneName== "CameraStarting")
        {
            nextSceneName = "Camera";
        }

        for (int i = 0; i < m_Icons.Count; i++)
        {
            if (m_Icons[i].gameObject.name == nextSceneName)
            {
                //選択できないようにする
                m_Icons[i].interactable = false;
            }
            else
            {
                //選択できるようにする
                m_Icons[i].interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string loadSceneName)
    {
        if (fadeCoroutine != null) return;

        transitionCoroutine = StartCoroutine(SceneTransition(m_TransitionTime, loadSceneName));
    }

    IEnumerator SceneTransition(float transitionTime, string loadSceneName)
    {
        ChangeIconActive(loadSceneName);
        fadeCoroutine = StartCoroutine(FadeIn(transitionTime));
        yield return fadeCoroutine;

        try
        {
            SceneManager.LoadSceneAsync(loadSceneName);
        }
        catch
        {
            Debug.Log("LoadSceneNotFound");
            yield break;
        }

        transitionCoroutine = null;
    }

    IEnumerator FadeIn(float transitionTime)
    {
        fade.gameObject.SetActive(true);
        float t = 0.0f;

        while (true)
        {
            t += Time.deltaTime;
            fade.color =new Color(1,1,1,(t/transitionTime));

            if (t > transitionTime) break;
            yield return null;
        }

        fadeCoroutine = null;
    }

    IEnumerator FadeOut(float transitionTime)
    {
        float t = 0.0f;

        while (true)
        {
            t += Time.deltaTime;
            //fade.fillAmount = 1.0f - t;
            fade.color = fade.color = new Color(1, 1, 1, 1.0f-(t / transitionTime));

            if (t > transitionTime) break;
            yield return null;
        }

        fade.gameObject.SetActive(false);
        fadeCoroutine = null;
    }
}
