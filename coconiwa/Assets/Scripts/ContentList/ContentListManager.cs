using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentListManager : MonoBehaviour
{
    [SerializeField]
    bool useSerializeData=false;

    [SerializeField]
    ContentsData contentsData = null;

    [SerializeField]
    ContentGroup ContentGroupA = null;

    [SerializeField]
    ContentGroup ContentGroupP = null;

    [SerializeField]
    ContentGroup ContentGroupI = null;

    [SerializeField]
    RectTransform contentRec = null;


    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        if (!useSerializeData)
        {
            contentsData = AppData.ContentsData;
        }
#else
        contentsData = AppData.ContentsData;
#endif

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
        ContentGroupP.Create();
        ContentGroupI.Create(ContentGroupP.mostUnderItem);
        ContentGroupA.Create(ContentGroupI.mostUnderItem);

        RectTransform rec = ContentGroupA.mostUnderItem.transform as RectTransform;
        RectTransform parentRec = rec.parent as RectTransform;
        float limit = rec.anchoredPosition.y + parentRec.anchoredPosition.y - 100.0f;
        Vector2 contentRecSize = contentRec.sizeDelta;
        contentRecSize.y = Mathf.Abs(limit);
        contentRec.sizeDelta = contentRecSize;
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
