using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsSwipeController : SingletonMonoBehaviour<ContentsSwipeController>
{
    public void SetImageNum(int imageNum)
    {
        //イメージの数が１枚以下ならスワイプする必要はない
        if (imageNum <= 1) return;

        //todo : イメージの数に合わせて丸ポチを配置する
    }
}
