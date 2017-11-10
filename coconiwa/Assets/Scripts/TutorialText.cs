using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

    [SerializeField]
     string[] Japanese  = null;
    [SerializeField]
    string[] English = null;
    [SerializeField]
    string[] Korea = null;
    [SerializeField]
    string[] ChinaSimplification = null;
    [SerializeField]
    string[] ChinaTraditional = null;

    [SerializeField]
    List<Text> texts = null;

    // Use this for initialization
    void Start () {
		switch(AppData.UsedLanguage)
        {
            case SystemLanguage.Japanese:
                SetText(Japanese);
                break;
            case SystemLanguage.Korean:
                SetText(Korea);
                break;
            case SystemLanguage.Chinese:
                SetText(ChinaSimplification);
                break;
            case SystemLanguage.ChineseSimplified:
                SetText(ChinaSimplification);
                break;
            case SystemLanguage.ChineseTraditional:
                SetText(ChinaTraditional);
                break;
            default:
                SetText(Japanese);
                break;
        }
    }


    private void SetText(string[] setTexts)
    {
        for (int i = 0; i < setTexts.Length; i++)
        {
            texts[i].text = setTexts[i];
        }
    }	
}
