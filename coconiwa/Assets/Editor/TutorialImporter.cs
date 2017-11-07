using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

namespace Assets.Editor
{
    class TutorialImporter
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            string targetFile = "Assets/CSV/Tutorial.csv";
            string exportFile = "Assets/CSV/Tutorial.asset";

            foreach (string asset in importedAssets)
            {
                //合致しないものはスルー
                if (!targetFile.Equals(asset)) continue;

                // 既存のマスタを取得
                TutorialData data = AssetDatabase.LoadAssetAtPath<TutorialData>(exportFile);

                // 見つからなければ作成する
                if (data == null)
                {
                    data = ScriptableObject.CreateInstance<TutorialData>();
                    AssetDatabase.CreateAsset((ScriptableObject)data, exportFile);
                    AssetDatabase.SaveAssets();
                }
                else
                {
                    // 中身を削除
                    data.TextData.Clear();
                }
                //変更ここから//
                AssetDatabase.StartAssetEditing();
                // CSVファイルをオブジェクトへ保存
                using (StreamReader sr = new StreamReader(targetFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] dataStrs = line.Split(',');
                        //言語名(SystemLanguageの文字列にする),見出し,テキストにする
                        TutorialData.Params p = new TutorialData.Params();
                        p.Name = dataStrs[1];
                        p.Text = dataStrs[2];
                        SystemLanguage language = (SystemLanguage)Enum.ToObject(typeof(SystemLanguage), dataStrs[0]);
                        data.TextData.Add(language,p);
                    }
                }

                //変更ここまで//
                AssetDatabase.StopAssetEditing();

                //変更をUnityEditorに伝える//
                EditorUtility.SetDirty(data);

                //すべてのアセットを保存//
                AssetDatabase.SaveAssets();

                Debug.Log("TutorialCSV updated.");
            }
        }
    }
}
