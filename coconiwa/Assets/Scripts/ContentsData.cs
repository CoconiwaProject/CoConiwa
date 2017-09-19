using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ContentsType
{
}

public class ContentsData : ScriptableObject
{
    [System.Serializable]
    public class Params
    {
        public string FileID;
        public string ContentsName;
        public string ContentsText;
    }

    public List<Params> Elements = new List<Params>();

    Dictionary<string, Params> contentDictionary = new Dictionary<string, Params>();
    public Dictionary<string, Params> ContentDictionary
    {
        get
        {
            //まだデータがセットされていなかったら
            if (contentDictionary.Count == 0)
            {
                AddDictionary(Elements);
            }

            return contentDictionary;
        }
    }

    void AddDictionary(List<Params> elements)
    {
        if (elements.Count == 0)
        {
            contentDictionary = null;
            return;
        }

        for (int i = 0; i < elements.Count; i++)
        {
            contentDictionary.Add(elements[i].FileID, elements[i]);
        }
    }
}
