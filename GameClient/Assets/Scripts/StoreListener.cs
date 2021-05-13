using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

/// <summary>
/// ストアのコールバック
/// </summary>
public class StoreListener : MonoBehaviour, IStoreListener
{
    /// <summary>
    /// ドロップダウン
    /// </summary>
    [SerializeField]
    private Dropdown ItemDropdown = null;

    /// <summary>
    /// アイテム名からＩＤに変換するDictionary
    /// </summary>
    private Dictionary<string, string> NameToIdDic = new Dictionary<string, string>();

    /// <summary>
    /// ＩＤからProductに変換するDictionary
    /// </summary>
    private Dictionary<string, Product> IdToProductDic = new Dictionary<string, Product>();

    /// <summary>
    /// ストアコントローラ
    /// </summary>
    private IStoreController StoreControlller = null;

    void Awake()
    {
        StartCoroutine(APICall.GetPayItemList((Items) =>
        {
            Initialize(Items);
        }));
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="Items">課金アイテムのリスト</param>
    private void Initialize(PayItem[] Items)
    {
        var Module = StandardPurchasingModule.Instance();
        Module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser; //Unityエディタでのダミー表示
        var Builder = ConfigurationBuilder.Instance(Module);
        foreach (var Item in Items)
        {
            NameToIdDic.Add(Item.name, Item.id);
            Builder.AddProduct(Item.id, ProductType.Consumable);
            ItemDropdown.options.Add(new Dropdown.OptionData(Item.name));
        }
        UnityPurchasing.Initialize(this, Builder);
        Debug.Log("StoreListener Initialized.");
    }

    /// <summary>
    /// 選択されたアイテムを購入
    /// </summary>
    public void BuySelectedItem()
    {
        // アイテム名からIDを求め、さらにProductを求める
        var Name = ItemDropdown.options[ItemDropdown.value].text;
        var Id = NameToIdDic[Name];
        var Prod = IdToProductDic[Id];
        try
        {
            StoreControlller.InitiatePurchase(Prod);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// IAP初期化完了コールバック
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        StoreControlller = controller;
        foreach (var Prod in controller.products.all)
        {
            IdToProductDic.Add(Prod.definition.id, Prod);
        }
    }

    /// <summary>
    /// IAP初期化失敗コールバック
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    /// <summary>
    /// 購入失敗コールバック
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase Failed. Fuck!");
    }

    /// <summary>
    /// 購入成功コールバック
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        var Result = PurchaseProcessingResult.Pending;
        Debug.Log("Receipt:" + e.purchasedProduct.receipt);
        StartCoroutine(APICall.VerifyReceipt(e.purchasedProduct.receipt, (isSuccess) =>
        {
            if (isSuccess)
            {
                Debug.Log("購入成功");
                Result = PurchaseProcessingResult.Complete;
            }
            else
            {
                Debug.Log("Fuck!!");
            }
        }));
        return Result;
    }
}
