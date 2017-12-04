using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutSceneManager : MonoBehaviour
{
    [SerializeField]
    string[] fileIDList = null;

    ContentsData contentsData = null;

    [SerializeField]
    AboutItem itemPrefab = null;

    [SerializeField]
    Transform itemContainer = null;

    [SerializeField]
    DeveloperMenu developerMenu = null;

    [SerializeField]
    CreditController creditController = null;

    void Start()
    {
        Font Arial=null;
        if (AppData.ChangeFont)
            Arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        contentsData = AppData.ContentsData;
        for (int i = 0; i < fileIDList.Length; i++)
        {
            AboutItem item = Instantiate(itemPrefab, itemContainer);
            item.Init(contentsData.ContentDictionary[fileIDList[i]].ContentsName, fileIDList[i]);
            if (AppData.ChangeFont)
                item.title.font = Arial;
        }

        AboutItem credit = Instantiate(itemPrefab, itemContainer);
        credit.Init(AppData.UsedLanguage == SystemLanguage.Japanese ? "クレジット" : "Credit",
            () =>
            {
                creditController.PushCredit();
            });
        if (AppData.ChangeFont)
            credit.title.font = Arial;


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
