using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

/// <summary>
/// アプリケーションキー等を設定するクラス
/// </summary>
public class KeyInjection : MonoBehaviour
{
    void Awake()
    {
        NCMBSettings.ApplicationKey = EnvironmentDatas.ApplicationKey;
        NCMBSettings.ClientKey = EnvironmentDatas.ClientKey;
        GetComponent<NCMBSettings>().Init();
    }
}
