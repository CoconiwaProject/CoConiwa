using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMaker : MonoBehaviour
{
    Button m_MakerButton;

    [SerializeField]
    Button m_MakerName;

    [SerializeField]
    Text text;

    [SerializeField]
    string m_FileID;
	// Use this for initialization
	void Start ()
    {
        m_MakerButton = GetComponent<Button>();
        text.text =  GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>().m_ContentsData.Elements.Find(x =>  x.FileID==m_FileID).ContentsName;
        if (PlayerPrefs.GetInt("GetContents" + m_FileID) == 0)
        {
            m_MakerButton.onClick.AddListener(NameOpen);
            m_MakerName.onClick.AddListener(MapSceneLoad);
        }
        else
        {
            gameObject.SetActive(false);
        }
        m_MakerName.gameObject.SetActive(false);
    }

	
    void MapSceneLoad()
    {
        AppData.SelectTargetName = m_FileID;
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoadAttach>().MapSceneLoad(m_FileID);
    }

    void NameOpen()
    {
        m_MakerName.gameObject.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
