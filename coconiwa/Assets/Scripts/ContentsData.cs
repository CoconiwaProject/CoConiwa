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

    public List<Params> Elements;
}
