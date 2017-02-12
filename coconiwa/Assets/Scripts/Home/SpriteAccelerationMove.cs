using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAcceraletionMove : MonoBehaviour
{

    float m_sideAcceleration = 0.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_sideAcceleration += Input.acceleration.x;

        transform.localPosition += Vector3.right * m_sideAcceleration;

       
	}
}
