using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderBerMenu : MonoBehaviour
{
    [SerializeField]
    Button[] m_Icons;

	// Use this for initialization
	void Start ()
    {
        m_Icons = transform.GetComponentsInChildren<Button>();
        ActiveIconDisable();
	}

    void ActiveIconDisable()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        for(int i=0;i<m_Icons.Length;i++)
        {

            if(m_Icons[i].gameObject.name==sceneName)
            {
                m_Icons[i].interactable = false;
                m_Icons[i].image.sprite = m_Icons[i].spriteState.pressedSprite;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
