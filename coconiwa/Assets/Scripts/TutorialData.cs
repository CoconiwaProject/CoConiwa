using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class TutorialData : ScriptableObject
{
    public class Params
    {
        public string Name;
        public string Text;
    }
   
    public Dictionary<SystemLanguage,Params> TextData = new Dictionary<SystemLanguage, Params>();
}

