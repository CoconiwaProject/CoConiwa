using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeMultiLanguageFont : MonoBehaviour
{

    [SerializeField]
    List<Text> ChangeText = new List<Text>();

    Font Arial = null;

    // Use this for initialization
    void Start()
    {
 // if(AppData.ChangeFont)
        if(true)
        { 
            Arial = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

            for (int i = 0; i < ChangeText.Count; i++)
            {
                ChangeText[i].font = Arial;
            }
        }
    }
}
