using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentListManager : MonoBehaviour
{
    [SerializeField]
    ContentsData contentsData;

    [SerializeField]
    ContentGroup ContentGroupA;

    [SerializeField]
    ContentGroup ContentGroupP;

    [SerializeField]
    ContentGroup ContentGroupI;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < contentsData.Elements.Count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>(contentsData.Elements[i].FileID);
            string name = contentsData.Elements[i].ContentsName;

            char h = contentsData.Elements[i].FileID[0];

            //タイプによって画像、色の変更
            if (h == 'A')
            {
                ContentGroupA.contentParams.Add(contentsData.Elements[i]);

            }
            else if (h == 'P')
            {
                ContentGroupP.contentParams.Add(contentsData.Elements[i]);

            }
            else if (h == 'L')
            {
                ContentGroupI.contentParams.Add(contentsData.Elements[i]);
            }
        }

        ContentGroupA.Create();
        ContentGroupP.Create();
        ContentGroupI.Create();
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
        return -1;
    }
}
