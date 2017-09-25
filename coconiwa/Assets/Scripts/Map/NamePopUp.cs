﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NamePopUp : MonoBehaviour
{
    string fileID = "";

    //Noneの場合はLの画像を使用する
    private enum MakerType { P, A, L, None }
    public enum MakerSize { S, M, L, None }

    void Start()
    {
    }

    public void SetText(string fileID)
    {
        this.fileID = fileID;
        SetPopUpTexture(GetMakerType(fileID), GetMakerSize(MapManager.I.GetContentName(fileID)));
        GetComponentInChildren<Text>().text = MapManager.I.GetContentName(fileID);
        //todo:テキストの長さに応じてサイズ変更
    }

    public void MapSceneLoad()
    {
        AppData.SelectTargetName = fileID;
        UnderBerMenu.I.ChangeScene("Content");
    }

    void SetPopUpTexture(MakerType type, MakerSize size)
    {
        //表示するテキストが存在しない
        if (size == MakerSize.None) return;

        //Lに変換
        if (type == MakerType.None)
        {
            type = MakerType.L;
        }
        int index = ((int)type * (int)MakerType.None) + (int)size;
        transform.GetChild(0).GetComponent<Image>().sprite = MapManager.I.balloonImageList[index];
    }

    MakerType GetMakerType(string fileID)
    {
        if (fileID[0] == 'A') return MakerType.A;
        else if (fileID[0] == 'L') return MakerType.L;
        else if (fileID[0] == 'P') return MakerType.P;

        return MakerType.None;
    }

    public static MakerSize GetMakerSize(string fileName)
    {
        if (fileName.Length > 7)
        {
            return MakerSize.L;
        }
        else if (fileName.Length > 4)
        {
            return MakerSize.M;
        }
        else
        {
            return MakerSize.S;
        }
    }
}
