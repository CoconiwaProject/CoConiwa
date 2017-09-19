using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutSceneManager : MonoBehaviour
{
    [SerializeField]
    string[] fileIDList = null;

    [SerializeField]
    ContentsData contentsData = null;

    [SerializeField]
    AboutItem itemPrefab = null;

    [SerializeField]
    Transform itemContainer = null;

    [SerializeField]
    DeveloperMenu developerMenu = null;

    void Start()
    {
        for(int i = 0;i< fileIDList.Length;i++)
        {
            AboutItem item = Instantiate(itemPrefab, itemContainer);
            item.Init(contentsData.ContentDictionary[fileIDList[i]].ContentsName, fileIDList[i]);
        }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        AboutItem workSheet = Instantiate(itemPrefab, itemContainer);
        workSheet.Init("アンケート回答",
            () =>
            {
                developerMenu.StartWorkSheet();
            });

        AboutItem developMenu = Instantiate(itemPrefab, itemContainer);

        developMenu.Init("開発者向け",
            () =>
            {
                developerMenu.Open();
            });
#endif
    }
}
