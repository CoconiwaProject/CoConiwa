using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class TitleSceneLoad : MonoBehaviour
{
    public Image Panel;
    public string SceneName;
    public float duration = 1.0f;

    [SerializeField]
    ContentsData JapaneseData;
    [SerializeField]
    ContentsData EnglishData;
    [SerializeField]
    ContentsData KoreaData;
    [SerializeField]
    ContentsData ChinaSimplificationData;
    [SerializeField]
    ContentsData ChinaTraditionalData;

    private void Start()
    {
        SetLanguage();
    }

    public void NextScene()
    {
        if (SceneLoadManager.I.IsFading) return;

        UnderBerMenu menu = UnderBerMenu.I;
        //menuをpanelの後ろに持ってくる
        menu.transform.SetSiblingIndex(0);

        bool isFirst = false;
        if (!PlayerPrefs.HasKey("Init"))
        {
            isFirst = true;
            SceneName = "Tutorial";
        }

        SceneLoadManager.I.SceneTransition(SceneName, () =>
        {
            if (isFirst)
                menu.SetUnderBerActive(false);
            else
                menu.SetUnderBerActive(true);

        }, () =>
        {
            if (!isFirst)
                menu.transform.SetSiblingIndex(1);
        }, duration);

    }

    private void SetLanguage()
    {
        string exportFile = "Assets/CSV/Data.asset";
        ContentsData data = AssetDatabase.LoadAssetAtPath<ContentsData>(exportFile);
        data.Elements.Clear();
        AssetDatabase.StartAssetEditing();

        SystemLanguage nowLanguage = Application.systemLanguage;
        if (nowLanguage == SystemLanguage.Japanese)
        {
            SetParams(data, KoreaData);
            AppData.UsedLanguage = SystemLanguage.Korean;//いったんテスト
            //AppData.UsedLanguage = SystemLanguage.Japanese;
            //  foreach (ContentsData.Params p in JapaneseData.Elements)
            //      data.Elements.Add(p);
        }
        else if (nowLanguage == SystemLanguage.ChineseSimplified)
        {
            AppData.UsedLanguage = SystemLanguage.ChineseSimplified;
            SetParams(data,ChinaSimplificationData);
            
        }
        else if (nowLanguage == SystemLanguage.ChineseTraditional || nowLanguage == SystemLanguage.Chinese)
        {
            AppData.UsedLanguage = SystemLanguage.ChineseTraditional;
            SetParams(data, ChinaTraditionalData);
        }
        else if (nowLanguage == SystemLanguage.Korean)
        {
            AppData.UsedLanguage = SystemLanguage.Korean;
            SetParams(data,KoreaData);
        }
        else
        {
            AppData.UsedLanguage = SystemLanguage.English;
            SetParams(data, EnglishData);
        }

        //変更ここまで//
        AssetDatabase.StopAssetEditing();
        //変更をUnityEditorに伝える//
        EditorUtility.SetDirty(data);
        //すべてのアセットを保存//
        AssetDatabase.SaveAssets();
    }

    //日本語であるもののみ入れる
    private void SetParams(ContentsData data, ContentsData addData)
    {
        foreach (ContentsData.Params p in JapaneseData.Elements)
        {
            foreach (ContentsData.Params j in addData.Elements)
            {
                if (p.FileID == j.FileID)
                {
                    data.Elements.Add(j);
                    break;
                }
            }
        }
    }

}
