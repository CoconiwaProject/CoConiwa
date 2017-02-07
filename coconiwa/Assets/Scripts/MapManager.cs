using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    GameObject Map = null;

    [SerializeField]
    ContentsData contentsData;

    public Image namePopUp = null;

    public static MapManager I = null;

    List<MapMaker> makerList = new List<MapMaker>();

    [SerializeField]
    public ContentsData m_ContentsData;

    //カメラ視覚の範囲
    float vMin = 1.0f;
    float vMax = 5.0f;
    //初期値
    float v = 1.0f;

    //最初にタッチした時の2点間の距離.
    private float backDist = 0.0f;

    //指を離した時間
    private Timer separateTime = new Timer();
    
    void Awake()
    {
        if(I != null)
        {
            Destroy(I);
            return;
        }

        I = this;
    }

    void Start()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(GameObject.FindGameObjectsWithTag("Maker"));
        for (int i = 0; i < tempList.Count; i++)
        {
            makerList.Add(tempList[i].GetComponent<MapMaker>());
        }
        TouchManager.Instance.Drag += OnMapSwipe;
    }

    private void OnDestroy()
    {
        TouchManager.Instance.Drag -= OnMapSwipe;
    }

    void OnMapSwipe(object sender, CustomInputEventArgs e)
    {

        this.Map.transform.position += new Vector3(e.Input.DeltaPosition.x, e.Input.DeltaPosition.y, 0) * 5.0f;

        float t = 1000.0f * Map.transform.localScale.x;
        float yt = 500.0f * Map.transform.localScale.y;

        if (Map.transform.localPosition.x > t)
        {
            Map.transform.localPosition = new Vector3(t, Map.transform.localPosition.y, Map.transform.localPosition.z);
        }
        if (Map.transform.localPosition.x < -t)
        {
            Map.transform.localPosition = new Vector3(-t, Map.transform.localPosition.y, Map.transform.localPosition.z);
        }

        if (Map.transform.localPosition.y > yt)
        {
            Map.transform.localPosition = new Vector3(Map.transform.localPosition.x, yt, Map.transform.localPosition.z);
        }
        if (Map.transform.localPosition.y < -yt)
        {
            Map.transform.localPosition = new Vector3(Map.transform.localPosition.x, -yt, Map.transform.localPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        separateTime.Update();
        if (separateTime.IsLimitTime) separateTime.Stop(true);

        if (!Application.isEditor)
        {
            v += GetPinchValue();
        }
        else
        {
            v += Input.mouseScrollDelta.y * 0.2f;
        }
        
        // 限界値をオーバーした際の処理
        if (v > vMax)
        {
            if(Input.touchCount <= 0)
            {
                if(!separateTime.IsWorking) separateTime.TimerStart(1.0f);
                v = FloatLerp(v, vMax, separateTime.Progress);
            }

            v = Mathf.Min(v, vMax + 3.0f);
        }
        else if (v < vMin)
        {
            if (Input.touchCount <= 0)
            {
                if (!separateTime.IsWorking) separateTime.TimerStart(1.0f);
                v = FloatLerp(v, vMin, separateTime.Progress);
            }

            v = Mathf.Max(v, vMin * 0.5f);
        }

        // 相対値が変更した場合、カメラに相対値を反映させる
        if (v != 0)
        {
            Map.transform.localScale = new Vector3(v, v, 1.0f);
        }
    }

    //２本の指の間隔を広くした時がプラス、狭めた時がマイナスの値になる
    float GetPinchValue()
    {
        if (Input.touchCount < 2) return 0.0f;

        // タッチしている２点を取得
        Touch t1 = Input.GetTouch(0);
        Touch t2 = Input.GetTouch(1);

        //2点タッチ開始時の距離を記憶
        if (t2.phase == TouchPhase.Began)
        {
            backDist = Vector2.Distance(t1.position, t2.position);
            return 0.0f;
        }
        else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
        {
            float speed = 0.0001f;
            if(t1.phase == TouchPhase.Stationary)
            {
                speed = 0.0002f;
            }

            // タッチ位置の移動後、長さを再測し、前回の距離からの相対値を取る。
            return (Vector2.Distance(t1.position, t2.position) - backDist) * speed;
        }

        return 0.0f;
    }

    public float FloatLerp(float a, float b, float t)
    {
        t = Mathf.Clamp(t, 0, 1);

        return a + ((b - a) * t);
    }

    public string GetContentName(string name)
    {
        int index = contentsData.Elements.FindIndex(n => n.FileID == name);

        if (index == -1) return "エラー";

        return contentsData.Elements[index].ContentsName;
    }

    public void TouchMaker()
    {
        for(int i = 0;i < makerList.Count;i++)
        {
            makerList[i].IsSelect = false;
        }   
    }
}
