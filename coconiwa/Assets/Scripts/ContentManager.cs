using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    [SerializeField]
    ContentsData contentsData;

    [SerializeField]
    Image m_Image;

    [SerializeField]
    Text ContentName;

    [SerializeField]
    Text ContentText;
    [SerializeField]
    Image ContentsBack;

    [SerializeField]
    Image Header;

    [SerializeField]
    Sprite Inter;
    [SerializeField]
    Sprite Artfact;
    [SerializeField]
    Sprite Prants;

    private int index;

    // Use this for initialization
    void Start ()
    {
        index = GetIndex(AppData.SelectTargetName);
        if (index == -1) return;
        m_Image.sprite = Resources.Load<Sprite>(contentsData.Elements[index].FileID);
        ContentName.text = contentsData.Elements[index].ContentsName;
        ContentText.text = contentsData.Elements[index].ContentsText;

        char h= contentsData.Elements[index].FileID[0];

        //タイプによって画像、色の変更
        if (h=='A')
        {
            Header.sprite = Artfact;
            ContentsBack.color = new Color(0.65f, 0.25f, 0.0f);
        }
        else if(h=='P')
        {
            Header.sprite = Prants;
        }
        else if(h=='I')
        {
            Header.sprite = Inter;
            ContentsBack.color = new Color(0.19f, 0.3f, 0.54f);
        }


	}
	

   int GetIndex(string name)
    {
        for(int i=0;i<contentsData.Elements.Count;i++)
        {
            if(contentsData.Elements[i].FileID==name)
            {
                return i;
            }
        }
        Debug.LogError("NotFoundData");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        return -1;
    }
}
