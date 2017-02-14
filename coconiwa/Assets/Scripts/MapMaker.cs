using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapMaker : MonoBehaviour
{
    public string fileID = "";
    public bool IsSelect = false;

    public void Touch()
    {
        if (IsSelect)
        {
            MapSceneLoad();
            return;
        }

        MapManager.I.TouchMaker(fileID);
        IsSelect = true;
        Image namePopUp = MapManager.I.namePopUp;
        namePopUp.gameObject.SetActive(true);
        namePopUp.rectTransform.anchoredPosition = GetPopUpPosition();
        namePopUp.GetComponent<NamePopUp>().SetText(fileID);
    }

    Vector2 GetPopUpPosition()
    {
        //todo:画面端の確認
        return GetComponent<RectTransform>().anchoredPosition + (Vector2.up * 50.0f);
    }

    void MapSceneLoad()
    {
        AppData.SelectTargetName = fileID;
        GameObject.Find("Canvas2").GetComponentInChildren<UnderBerMenu>().ChangeScene("Content");
    }
}
