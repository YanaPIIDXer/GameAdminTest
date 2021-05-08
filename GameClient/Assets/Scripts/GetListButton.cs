using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

/// <summary>
/// 課金アイテムリスト取得ボタン
/// </summary>
public class GetListButton : MonoBehaviour
{
    /// <summary>
    /// リスト表示用のView
    /// ※汚い設計だけど仕方なし
    /// </summary>
    [SerializeField]
    private GameObject TargetView = null;

    /// <summary>
    /// 要素のPrefab
    /// </summary>
    private static GameObject ItemPrefab = null;

    /// <summary>
    /// 押された
    /// </summary>
    public void OnPressed()
    {
        while (TargetView.transform.childCount > 0)
        {
            var Child = TargetView.transform.GetChild(0);
            GameObject.Destroy(Child);
        }
        TargetView.transform.DetachChildren();

        if (ItemPrefab == null)
        {
            ItemPrefab = Resources.Load<GameObject>("Prefabs/PayItemButton");
            Debug.Assert(ItemPrefab != null);
        }

        StartCoroutine(APICall.GetPayItemList((Items) =>
        {
        }));
    }
}
