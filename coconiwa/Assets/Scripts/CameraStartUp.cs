using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartUp : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Camera");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
