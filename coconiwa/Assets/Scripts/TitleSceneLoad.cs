using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleSceneLoad : MonoBehaviour
{
    public Image Panel;
    public string SceneName;
    public float duration = 1.0f;

    public void NextScene()
    {
        if (!PlayerPrefs.HasKey("Init")) SceneName = "Tutorial";

        UnderBerMenu menu = UnderBerMenu.I;
        //menuをpanelの後ろに持ってくる
        menu.transform.SetSiblingIndex(0);

        SceneLoadManager.I.SceneTransition(SceneName, () =>{
            menu.SetUnderBerActive(true);
        }, () =>{
            menu.transform.SetSiblingIndex(1);
        },duration);

    }

    IEnumerator IE_NextSceneLoad()
    {
        float t =0;
        while(true)
        {
            t += Time.deltaTime;
            float n = t / duration;
            Panel.color = new Color(1,1,1,n*n);
            if (t >= duration) break;
            yield return null;
        }
        if (!PlayerPrefs.HasKey("Init"))
            SceneName = "Tutorial";
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        yield return null;
    }
}
