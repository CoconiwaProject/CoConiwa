using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnderBerMenu : SingletonMonoBehaviour<UnderBerMenu>
{
    List<Button> m_Icons = new List<Button>();

    [SerializeField]
    Image fade = null;

    AudioSource m_audioSource;

    Coroutine fadeCoroutine;
    Coroutine transitionCoroutine;

    float m_TransitionTime = 0.3f;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(transform.parent.gameObject);
        m_Icons.AddRange(transform.GetComponentsInChildren<Button>());
        m_audioSource = GetComponent<AudioSource>();
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

    public void ChangeScene(string loadSceneName)
    {
        if (fadeCoroutine != null) return;

        transitionCoroutine = StartCoroutine(SceneTransition(m_TransitionTime, loadSceneName));
        m_audioSource.Play();
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

    Color clearColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    Color whiteColor = Color.white;

    MyCoroutine FadeIn(float transitionTime)
    {
        fade.gameObject.SetActive(true);
        return FadeAnimation(clearColor, whiteColor, transitionTime);
    }

    MyCoroutine FadeOut(float transitionTime)
    {
        return FadeAnimation(whiteColor, clearColor, transitionTime).OnCompleted(() => fade.gameObject.SetActive(false));
    }

    MyCoroutine FadeAnimation(Color startColor, Color endColor, float duration)
    {
        return KKUtilities.FloatLerp(duration, (t) =>
        {
            fade.color = Color.Lerp(startColor, endColor, t);
        }).OnCompleted(() =>
        {
            fadeCoroutine = null;
        });
    }
}
