using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Content");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
