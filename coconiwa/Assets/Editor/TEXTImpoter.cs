using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class TEXTImpoter : AssetPostprocessor
{

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {

        string targetFile = "Assets/CSV/ChinaTraditional.txt";
        string exportFile = "Assets/CSV/ChinaTraditional.asset";

        foreach (string asset in importedAssets)
        {
            // 合致しないものはスルー
            if (!targetFile.Equals(asset)) continue;

            // 既存のマスタを取得
            ContentsData data = AssetDatabase.LoadAssetAtPath<ContentsData>(exportFile);

            // 見つからなければ作成する
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<ContentsData>();
                AssetDatabase.CreateAsset((ScriptableObject)data, exportFile);
                AssetDatabase.SaveAssets();
            }
            else
            {

                // 中身を削除
                data.Elements.Clear();
            }
            //変更ここから//
            AssetDatabase.StartAssetEditing();
            // CSVファイルをオブジェクトへ保存
            using (StreamReader sr = new StreamReader(targetFile))
            {
                Debug.Log("textimport");
                string line;

                // ファイルの終端まで繰り返す
                while (!sr.EndOfStream)
                {
                    // 追加するパラメータを生成v
                    ContentsData.Params p = new ContentsData.Params();
                    // 値を設定する
                    line = sr.ReadLine();
                    p.FileID = line.Substring(0, 4);
                    p.ContentsName = ReturnIntervalString(line.Remove(0, 4));

                    line = sr.ReadLine();
                    p.ContentsText = ReturnIntervalString(line);
                    // 追加
                    data.Elements.Add(p);
                    sr.ReadLine();//改行分無視
                }
            }

            //変更ここまで//
            AssetDatabase.StopAssetEditing();

            //変更をUnityEditorに伝える//
            EditorUtility.SetDirty(data);

            //すべてのアセットを保存//
            AssetDatabase.SaveAssets();

            Debug.Log("Data updated.");
        }


    }

    //最初の空白文字を削除、kokoniwa修正
    static string ReturnIntervalString(string originalString)
    {
        Debug.Log("strat==="+originalString);
        string returnString = originalString;
       // Debug.Log("3");
       // int selectStrNum = 1;// returnString.IndexOf('　', ' ');
        //最初の空白文字を削除
        for (int i=0;i< originalString.Length;i++)
        {
            if (returnString[0] == ' ' || returnString[0] == '　')
            {
                returnString = returnString.Remove(0,1);
            }
            else
                break;
        }

     

        Debug.Log(returnString);
        return returnString.Replace("kokoniwa", "coconiwa");
    }

}