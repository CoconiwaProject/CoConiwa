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
        int oneLineSize = 22;//22以上だと一列、
        int towLineSize = 50;//50以上だと２列?

        float xScale = 2.2f;//一列の際のかけるサイズ
        float yScale = 2.2f;

        int xCount=0;
        int yCount = 0;

        for (int i = 0; i < contentParams.Count; i++)
        {
            Debug.Log("文字数=" + contentParams[i].ContentsName.Length + "=" + contentParams[i].ContentsName);
            ContentListItem item = Instantiate(ItemPrefab, transform);
            int strLength = contentParams[i].ContentsName.Length;
            if (strLength<=oneLineSize)
            {
                Debug.Log("少");
             item.transform.localPosition = new Vector3((xCount % 2) * 550.0f + 80.0f, -yCount * 100.0f, 0.0f);
                xCount++;
                if (xCount % 2 == 0)
                    yCount++;
            }
            else if(strLength<=towLineSize)
            {
                RectTransform r = item.gameObject.GetComponent<RectTransform>();
                r.sizeDelta = new Vector2(r.sizeDelta.x*xScale, r.sizeDelta.y);
                RectTransform textRect =item.text.gameObject.GetComponent<RectTransform>();
                textRect.sizeDelta = new Vector2(textRect.sizeDelta.x * xScale, textRect.sizeDelta.y);

                if (xCount % 2 == 1)
                    yCount++;
                xCount = 0;
                item.transform.localPosition = new Vector3( 360, -yCount * 100.0f , 0.0f);
                yCount++;
            }
            else
            {

                RectTransform r = item.gameObject.GetComponent<RectTransform>();
                r.sizeDelta = new Vector2(r.sizeDelta.x * xScale, r.sizeDelta.y* yScale);
                RectTransform textRect = item.text.gameObject.GetComponent<RectTransform>();
                textRect.sizeDelta = new Vector2(textRect.sizeDelta.x * xScale, textRect.sizeDelta.y * yScale);

                if (xCount % 2 == 1)
                    yCount++;
                xCount = 0;
                item.transform.localPosition = new Vector3(360, -yCount * 100.0f - 50, 0.0f);
                yCount += 2;
            }


            item.BGImage.sprite = itemBGImage;
            item.ContentSet(contentParams[i]);
        }


        //for (int i = 0; i < contentParams.Count; i++)
        //{
        //    Debug.Log("文字数="+ contentParams[i].ContentsName.Length+"="+ contentParams[i].ContentsName);


        //    ContentListItem item = Instantiate(ItemPrefab, transform);
        //    item.transform.localPosition = new Vector3((i % 2) * 550.0f + 80.0f, -(int)(i * 0.5f) * 100.0f, 0.0f);
        //    item.BGImage.sprite = itemBGImage;
        //    item.ContentSet(contentParams[i]);
        //}
    }
}
