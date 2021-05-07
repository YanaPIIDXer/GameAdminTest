using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バージョン管理に含めるべきではないデータ定義
/// </summary>
public interface IEnvironmentData
{
    /// <summary>
    /// アプリケーションキー
    /// </summary>
    string ApplicationKey { get; }

    string ClientKey { get; }
}

/// <summary>
/// バージョン管理に含めるべきではないデータへのアクセス
/// </summary>
public static class EnvironmentDatas
{
    /// <summary>
    /// IEnvironmentData実装オブジェクト
    /// </summary>
    public static IEnvironmentData Env { set; private get; }

    /// <summary>
    /// アプリケーションキー
    /// </summary>
    public static string ApplicationKey { get { return Env.ApplicationKey; } }

    /// <summary>
    /// クライアントキーs
    /// </summary>
    /// <value></value>
    public static string ClientKey { get { return Env.ClientKey; } }
}
