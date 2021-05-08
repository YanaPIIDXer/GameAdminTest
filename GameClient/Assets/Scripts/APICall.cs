using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 課金アイテムデータ
/// </summary>
public struct PayItem
{
    /// <summary>
    /// ID
    /// </summary>
    public string Id;

    /// <summary>
    /// 名前
    /// </summary>
    public string Name;

    /// <summary>
    /// 値段
    /// </summary>
    public int Price;
}

/// <summary>
/// APIの呼び出し
/// </summary>
public static class APICall
{
    /// <summary>
    /// エンドポイント
    /// </summary>
    private static readonly string Endpoint = "http://localhost/api/";

    /// <summary>
    /// 課金アイテムリスト取得
    /// </summary>
    /// <param name="Callback">コールバック</param>
    public static IEnumerator GetPayItemList(Action<List<PayItem>> Callback)
    {
        using (var Req = UnityWebRequest.Get(Endpoint + "pay_item_list.php"))
        {
            yield return Req.SendWebRequest();
            Debug.Log(Req.downloadHandler.text);
        }
    }
}
