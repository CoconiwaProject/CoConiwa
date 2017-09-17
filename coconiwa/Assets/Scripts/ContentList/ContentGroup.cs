using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroup : MonoBehaviour
{

    [SerializeField]
    ContentListItem ItemPrefab;

    public List<ContentsData.Params> contentParams = new List<ContentsData.Params>();

    public void Create()
    {
        for(int i=0;i< contentParams.Count;i++)
        {
            ContentListItem item =  Instantiate(ItemPrefab, transform);
            item.transform.localPosition = new Vector3((int)(i%3)*350,-(int)(i/3)*100,0);
            item.ContentSet(contentParams[i]);
        }
    }
}
