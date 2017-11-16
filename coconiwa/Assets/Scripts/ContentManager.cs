using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    ContentsData contentsData;

    [SerializeField]
    Transform imageContainer = null;

    const int maxImageNum = 3;
    Image[] images = null;

    public bool isMulti = false;

    [SerializeField]
    Text ContentName = null;

    [SerializeField]
    HyphenationJpn ContentText = null;
    [SerializeField]
    Image ContentsBack = null;

    [SerializeField]
    Image Header = null;

    [SerializeField]
    Sprite Inter = null;
    [SerializeField]
    Sprite Artfact = null;
    [SerializeField]
    Sprite Prants = null;

    [SerializeField]
    ContentsTextController contentsTextController = null;

    private int index;

    // Use this for initialization
    void Start()
    {
        contentsData = AppData.ContentsData;
        index = GetIndex(AppData.SelectTargetName);

        if (index == -1) return;

        string fileName = "";
        char c = 'b';

        images = new Image[maxImageNum];
        for (int i = 0; i < maxImageNum; i++)
        {
            images[i] = imageContainer.GetChild(i).GetComponent<Image>();
            fileName = contentsData.Elements[index].FileID;

            if (i > 0)
            {
                fileName = contentsData.Elements[index].FileID + c;
                c++;
            }

            Debug.Log(fileName);
            images[i].sprite = Resources.Load<Sprite>(fileName);
            if (images[i].sprite == null) images[i].gameObject.SetActive(false);
            else isMulti = true;
        }

        
        ContentName.text = contentsData.Elements[index].ContentsName;
        ContentText.GetText(contentsData.Elements[index].ContentsText);

        contentsTextController.SetTextInterval();
        char h = contentsData.Elements[index].FileID[0];

        //タイプによって画像、色の変更
        if (h == 'A')
        {
            Header.sprite = Artfact;
            ContentsBack.color = new Color(0.65f, 0.25f, 0.0f);
        }
        else if (h == 'P')
        {
            Header.sprite = Prants;
        }
        else if (h == 'I')
        {
            Header.sprite = Inter;
            ContentsBack.color = new Color(0.19f, 0.3f, 0.54f);
        }


    }


    int GetIndex(string name)
    {
        for (int i = 0; i < contentsData.Elements.Count; i++)
        {
            if (contentsData.Elements[i].FileID == name)
            {
                return i;
            }
        }
        Debug.LogError("NotFoundData");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        return -1;
    }
}
