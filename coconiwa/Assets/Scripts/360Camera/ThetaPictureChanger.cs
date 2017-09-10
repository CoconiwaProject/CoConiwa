using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThetaPictureChanger : MonoBehaviour
{
    [SerializeField]
    Button button1 = null;
    [SerializeField]
    Button button2 = null;

    void Start()
    {
        if (!AppData.CanChangePicture) return;
        
        button1.onClick.AddListener(() =>
        {
            button1.interactable = false;
            button2.interactable = true;

            ThetaViewSceneManager.I.ChangePicture(AppData.SelectThetaPictures[0]);
        });

        button2.onClick.AddListener(() =>
        {
            button1.interactable = true;
            button2.interactable = false;

            ThetaViewSceneManager.I.ChangePicture(AppData.SelectThetaPictures[1]);
        });

        button1.interactable = false;
    }

}
