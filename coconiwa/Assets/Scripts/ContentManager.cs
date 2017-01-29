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

    private int index;

    // Use this for initialization
    void Start ()
    {
        index = GetIndex(AppData.SelectTargetName);
        m_Image.sprite = Resources.Load<Sprite>(contentsData.Elements[index].FileID);
        ContentName.text = contentsData.Elements[index].ContentsName;
        ContentText.text = contentsData.Elements[index].ContentsText;
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
