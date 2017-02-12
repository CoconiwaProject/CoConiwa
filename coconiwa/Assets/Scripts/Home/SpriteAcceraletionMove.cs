using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAccelerationMove : MonoBehaviour
{
    [SerializeField]
    float Threshold = 0.1f;
    [SerializeField]
    float xMaximum = 500.0f;

    [SerializeField]
    float xMinimum = -500.0f;


    float m_sideAcceleration = 0.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SideMove();
	}

    void SideMove()
    {
        if (Mathf.Abs(Input.acceleration.x) < Threshold) return;

        m_sideAcceleration += Input.acceleration.x;

        transform.localPosition += new Vector3(m_sideAcceleration,0,0);

        //maximum
        if(transform.localPosition.x>xMaximum)
        {
            transform.localPosition = new Vector3( xMaximum,transform.localPosition.y,transform.localPosition.z);
            m_sideAcceleration = 0.0f;
        }

        //minmum
        if (transform.localPosition.x < xMinimum)
        {
            transform.localPosition = new Vector3(xMinimum, transform.localPosition.y, transform.localPosition.z);
            m_sideAcceleration = 0.0f;
        }
    }
}
