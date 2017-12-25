using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMode : MonoBehaviour
{

    private static bool _isIphoneX = true;

    [RuntimeInitializeOnLoadMethod]
    void Init()
    {
#if UNITY_IPHONE
        string deviceName =  UnityEngine.iOS.DeviceGeneration.iPhoneX.ToString();
        _isIphoneX = deviceName.Equals("iPhoneX");
#endif
    }

    // Use this for initialization
    void Start ()
    {
        //状況を見て縦と横のどちらを優先するか決定する
	  if(_isIphoneX)
        {
            GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }	
      else
        {
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
	}
}
