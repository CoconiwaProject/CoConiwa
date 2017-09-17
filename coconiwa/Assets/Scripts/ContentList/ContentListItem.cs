using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentListItem : MonoBehaviour
{

    public Image BGImage;
    public Text text;
    public Button button;

    ContentsData.Params m_params;

    //発見フラグ
    bool isActive = false;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ContentSet(ContentsData.Params param)
    {
        if (PlayerPrefs.GetInt("GetContents" + param.FileID) != 0)
        {
            isActive = true;
            button.interactable = true;
        }

        text.text = param.ContentsName;
        m_params = param;
    }

    public void JumpScene()
    {
        AppData.SelectTargetName = m_params.FileID;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Content");
    }
}
