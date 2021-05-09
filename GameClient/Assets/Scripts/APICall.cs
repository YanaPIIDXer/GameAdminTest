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
    /// 課金アイテムリスト取得
    /// </summary>
    /// <param name="Callback">コールバック</param>
    public static IEnumerator GetPayItemList(Action<PayItem[]> Callback)
    {
        using (var Req = UnityWebRequest.Get("https://1bvwiok6dc.execute-api.ap-northeast-1.amazonaws.com/default/pay_item_list"))
        {
            yield return Req.SendWebRequest();

            Debug.Log(Req.downloadHandler.text);
            PayItem[] ItemList = JsonHelper.FromJson<PayItem>(Req.downloadHandler.text);
            Callback?.Invoke(ItemList);
        }
    }

    public static IEnumerator VerifyReceipt(string Receipt, Action<bool> Callback)
    {
        using (var Req = UnityWebRequest.Post("https://4ipqjo74e7.execute-api.ap-northeast-1.amazonaws.com/default/verify_receipt", "POST"))
        {
            byte[] PostData = System.Text.Encoding.UTF8.GetBytes(Receipt);
            Req.uploadHandler = (UploadHandler)new UploadHandlerRaw(PostData);
            Req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            Req.SetRequestHeader("Content-Type", "application/json");

            yield return Req.SendWebRequest();

            Debug.Log(Req.downloadHandler.text);
            VerifyReceiptResult Result = JsonUtility.FromJson<VerifyReceiptResult>(Req.downloadHandler.text);

            // とりあえず仮
            Callback?.Invoke(Result.result);
        }
    }
}
