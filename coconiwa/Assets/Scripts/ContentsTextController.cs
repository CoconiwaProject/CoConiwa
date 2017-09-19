using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContentsTextController : MonoBehaviour {


    [SerializeField]
    RectTransform[] setTextTransforms = null;

    [SerializeField]
    Text contentsText = null;

    Vector2 imageInterval=new Vector2(0,0);
    Vector2 titleInterval = new Vector2(0, 0);

    // Use this for initialization
    void Start () {
        titleInterval = new Vector2(setTextTransforms[0].position.x-contentsText.gameObject.transform.position.x, setTextTransforms[0].position.y - contentsText.gameObject.transform.position.y);
        imageInterval = new Vector2(setTextTransforms[1].position.x - contentsText.gameObject.transform.position.x, setTextTransforms[1].position.y - contentsText.gameObject.transform.position.y);
    }

    public void SetTextInterval()
    {
        contentsText.text = "\n\n" + contentsText.text;
    }

    // Update is called once per frame
    void Update () {
        setTextTransforms[0].transform.position = new Vector3(contentsText.gameObject.transform.position.x + titleInterval.x, contentsText.gameObject.transform.position.y + titleInterval.y, 0);
        setTextTransforms[1].transform.position = new Vector3(contentsText.gameObject.transform.position.x + imageInterval.x, contentsText.gameObject.transform.position.y + imageInterval.y, 0);
    }
}
