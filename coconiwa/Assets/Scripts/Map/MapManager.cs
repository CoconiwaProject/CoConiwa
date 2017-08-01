using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public float screenSizeRate = 1.0f;

    [SerializeField]
    ContentsData contentsData = null;

    public Image namePopUp = null;
    [SerializeField]
    List<Image> namePopUpList = new List<Image>();
    public List<Sprite> balloonImageList = new List<Sprite>();

    public static MapManager I = null;

    List<MapMaker> makerList = new List<MapMaker>();
    MapMaker currentMaker;
    public Sprite notFindImage = null;

    bool isTap = false;

    void Awake()
    {
        if (I != null)
        {
            Destroy(I);
            return;
        }

        I = this;
        //とりあえず横だけ
        screenSizeRate = 1.0f + (1.0f - ((float)Screen.width / 1080));
    }

    void Start()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(GameObject.FindGameObjectsWithTag("Maker"));
        for (int i = 0; i < tempList.Count; i++)
        {
            makerList.Add(tempList[i].GetComponent<MapMaker>());
        }
        TouchManager.Instance.TouchEnd += OnTouchEnd;
        TouchManager.Instance.TouchStart += OnTouchStart;
    }

#if UNITY_EDITOR
#else
    void OnDestroy()
    {
        TouchManager.Instance.TouchEnd -= OnTouchEnd;
        TouchManager.Instance.TouchStart -= OnTouchStart;
    }
#endif

    public string GetContentName(string name)
    {
        int index = contentsData.Elements.FindIndex(n => n.FileID == name);

        if (index == -1) return "エラー";

        return contentsData.Elements[index].ContentsName;
    }

    public void TouchMaker(string fileID, MapMaker maker)
    {
        isTap = false;
        currentMaker = maker;
        for (int i = 0; i < makerList.Count; i++)
        {
            makerList[i].IsSelect = false;
        }

        for (int i = 0; i < namePopUpList.Count; i++)
        {
            namePopUpList[i].gameObject.SetActive(false);
        }

        int index = (int)NamePopUp.GetMakerSize(GetContentName(fileID));
        namePopUp = namePopUpList[index];
    }

    void OnTouchStart(object sender, CustomInputEventArgs e)
    {
        isTap = true;
    }

    void OnTouchEnd(object sender, CustomInputEventArgs e)
    {
        //スワイプを検出されていればisTapはfalseになる
        if (isTap) Tapped();
    }

    //タップされた(スワイプ時には呼ばれない予定)
    void Tapped()
    {
        if (currentMaker == null) return;
        if (!currentMaker.IsSelect) return;

        currentMaker.IsSelect = false;
        namePopUp.gameObject.SetActive(false);
    }
}
