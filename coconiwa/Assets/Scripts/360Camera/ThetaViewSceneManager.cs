using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThetaViewSceneManager : SingletonMonoBehaviour<ThetaViewSceneManager>
{
    [SerializeField]
    Renderer sphereRenderer = null;

    [SerializeField]
    GameObject pictureChanger = null;

    protected override void Start()
    {
        base.Start();

        ChangePicture(AppData.SelectThetaPictures[0]);
        pictureChanger.SetActive(AppData.CanChangePicture);
    }

    public void ChangePicture(Texture tex)
    {
        sphereRenderer.material.mainTexture = tex;
    }
}
