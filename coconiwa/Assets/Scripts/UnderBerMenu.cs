using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UnderBerMenu : MonoBehaviour
{
    [SerializeField]
    Button[] m_Icons;

    [SerializeField]
    Image fade = null;

	// Use this for initialization
	void Start ()
    {
        m_Icons = transform.GetComponentsInChildren<Button>();
        ActiveIconDisable();
        StartCoroutine(FadeOut(1.0f));
	}

    void ActiveIconDisable()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        for(int i=0;i<m_Icons.Length;i++)
        {

            if(m_Icons[i].gameObject.name==sceneName)
            {
                m_Icons[i].interactable = false;
                m_Icons[i].image.sprite = m_Icons[i].spriteState.pressedSprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator FadeIn(float transitionTime, Action action)
    {
        fade.gameObject.SetActive(true);
        float t = 0.0f;
        while(true)
        {
            fade.fillAmount = t / transitionTime;
            t += Time.deltaTime;
            if (t > transitionTime) break;
            yield return null;
        }
        fade.gameObject.SetActive(false);

        action.Invoke();
    }

    public IEnumerator FadeOut(float transitionTime)
    {
        fade.gameObject.SetActive(true);
        float t = 0.0f;
        while (true)
        {
            fade.fillAmount = 1 - (t / transitionTime);
            t += Time.deltaTime;
            if (t > transitionTime) break;
            yield return null;
        }
        fade.gameObject.SetActive(false);
    }
}
