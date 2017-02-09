using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NamePopUp : MonoBehaviour
{
    string fileID = "";

    void Start()
    {
    }

    public void SetText(string fileID)
    {
        Debug.Log("fileID = " + fileID);
        this.fileID = fileID;
        GetComponentInChildren<Text>().text = MapManager.I.GetContentName(fileID);
        //todo:テキストの長さに応じてサイズ変更
    }

    public void MapSceneLoad()
    {
        AppData.SelectTargetName = fileID;
        GameObject.Find("Canvas2").GetComponentInChildren<UnderBerMenu>().ChangeScene(1.0f, "Content");
    }
}
