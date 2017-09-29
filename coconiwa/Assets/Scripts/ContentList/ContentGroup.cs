using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroup : MonoBehaviour
{

    [SerializeField]
    ContentListItem ItemPrefab;

    [SerializeField]
    Sprite itemBGImage;

    public List<ContentsData.Params> contentParams = new List<ContentsData.Params>();

    public void Create()
    {
        for (int i = 0; i < contentParams.Count; i++)
        {
            ContentListItem item = Instantiate(ItemPrefab, transform);
            item.transform.localPosition = new Vector3((i % 2) * 550.0f + 80.0f, -(int)(i * 0.5f) * 100.0f, 0.0f);
            item.BGImage.sprite = itemBGImage;
            item.ContentSet(contentParams[i]);
        }
    }
}
