using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 課金アイテムリスト取得ボタン
/// </summary>
public class GetListButton : MonoBehaviour
{
    /// <summary>
    /// 押された
    /// </summary>
    public void OnPressed()
    {
        StartCoroutine(APICall.GetPayItemList(null));
    }
}
