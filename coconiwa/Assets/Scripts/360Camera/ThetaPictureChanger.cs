using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ThetaPictureChanger : MonoBehaviour
{
    [SerializeField]
    RectTransform leftTextRec = null;

    [SerializeField]
    RectTransform rightTextRec = null;

    [SerializeField]
    RectTransform selectMaker = null;

    [SerializeField]
    Renderer sphereRenderer = null;

    Button button;
    bool isLeft = true;
    Coroutine positionControlCoroutine;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        if (!AppData.CanChangePicture) return;

        button.onClick.AddListener(() => ChangePicture());
    }

    MyCoroutine ChangeSelectMakerPosition(Vector2 targetPosition)
    {
        Vector2 startPosition = selectMaker.anchoredPosition;

        return KKUtilities.FloatLerp(0.2f, (t) =>
        {
            selectMaker.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
        }).OnCompleted(() => positionControlCoroutine = null);
    }

    void ChangePicture()
    {
        //反転
        isLeft = !isLeft;

        if(positionControlCoroutine != null)
        {
            StopCoroutine(positionControlCoroutine);
        }

        positionControlCoroutine = StartCoroutine(ChangeSelectMakerPosition(isLeft ? leftTextRec.anchoredPosition : rightTextRec.anchoredPosition).OnCompleted(() =>{
                SetPicture(AppData.SelectThetaPictures[isLeft ? 0 : 1]);
        }));

    }

    public void SetPicture(Texture tex)
    {
        sphereRenderer.material.mainTexture = tex;
    }
}
