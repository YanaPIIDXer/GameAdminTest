using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 課金アイテムデータ
/// </summary>
[Serializable]
public struct PayItem
{
    /// <summary>
    /// ID
    /// </summary>
    public string id;

    /// <summary>
    /// 名前
    /// </summary>
    public string name;

    /// <summary>
    /// 値段
    /// </summary>
    public int price;
}

/// <summary>
/// レシートチェックの結果
/// </summary>
[Serializable]
public struct VerifyReceiptResult
{
    public bool result;
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
    public static IEnumerator GetPayItemList(Action<PayItem[]> Callback)
    {
        using (var Req = UnityWebRequest.Get(Endpoint + "pay_item_list.php"))
        {
            yield return Req.SendWebRequest();

            PayItem[] ItemList = JsonHelper.FromJson<PayItem>(Req.downloadHandler.text);
            Callback?.Invoke(ItemList);
        }
    }

    public static IEnumerator VerifyReceipt(string Receipt, Action<bool> Callback)
    {
        using (var Req = UnityWebRequest.Post(Endpoint + "verify_receipt.php", Receipt))
        {
            yield return Req.SendWebRequest();

            VerifyReceiptResult Result = JsonUtility.FromJson<VerifyReceiptResult>(Req.downloadHandler.text);

            // とりあえず仮
            Callback?.Invoke(Result.result);
        }
    }
}
