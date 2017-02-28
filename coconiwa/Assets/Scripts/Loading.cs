using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Content");
       GameObject.FindGameObjectWithTag("Underber").GetComponent<UnderBerMenu>().ChangeIconActive("Content");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
