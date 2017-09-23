using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    void Start()
    {
        KKUtilities.WaitSeconde(0.5f, () => {
            SceneLoadManager.I.LoadSceneAsync("Content");
        }, this);
    }
}
