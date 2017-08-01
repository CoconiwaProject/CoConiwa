using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour
{    
    public string fileID = "";
    public bool IsSelect = false;
    private static Coroutine popUPCoroutine = null;

    void Start()
    {
        if (PlayerPrefs.GetInt("GetContents" + fileID) == 0)
        {
            //このマーカーはまだ見つけていないマーカー
            GetComponent<Image>().sprite = MapManager.I.notFindImage;
            GetComponent<Button>().enabled = false;
        }
    }

    public void Touch()
    {
        Debug.Log("call touch");
        if (IsSelect)
        {
            Debug.Log("call map scene load");
            MapSceneLoad();
            return;
        }

        MapManager.I.TouchMaker(fileID, this);
        IsSelect = true;
        Image namePopUp = MapManager.I.namePopUp;
        namePopUp.gameObject.SetActive(true);
        namePopUp.rectTransform.anchoredPosition = GetPopUpPosition();
        namePopUp.GetComponent<NamePopUp>().SetText(fileID);
        namePopUp.transform.localScale = Vector3.zero;

        if(popUPCoroutine != null)
        {
            StopCoroutine(popUPCoroutine);
        }

        popUPCoroutine = StartCoroutine(PopUp(namePopUp));
    }

    Vector2 GetPopUpPosition()
    {
        //todo:画面端の確認
        return GetComponent<RectTransform>().anchoredPosition + (Vector2.up * 75.0f);
    }

    void MapSceneLoad()
    {
        AppData.SelectTargetName = fileID;
        GameObject.Find("Canvas2").GetComponentInChildren<UnderBerMenu>().ChangeScene("Content");
    }

    IEnumerator PopUp(Image namePopUp)
    {
        float duration = 0.3f;
        float t = 0.0f;
        float progress = 0.0f;
        Vector2 startPosition = GetComponent<RectTransform>().anchoredPosition;
        Vector2 targetPosition = GetPopUpPosition();

        while(true)
        {
            t += Time.deltaTime;

            progress = t / duration;
            progress *= progress;
            namePopUp.rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            namePopUp.transform.localScale = Vector3.one * MapManager.I.FloatLerp(0.0f, 0.2f, progress);

            if (t > duration) break;
            yield return null;
        }

        popUPCoroutine = null;
    }
}
