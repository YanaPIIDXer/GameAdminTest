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

    /// <summary>
    /// クライアントキー
    /// </summary>
    string ClientKey { get; }
}

/// <summary>
/// バージョン管理に含めるべきではないデータへのアクセス
/// </summary>
public static class EnvironmentDatas
{
    /// <summary>
    /// IEnvironmentData実装オブジェクト
    /// HACK:元々static変数で自身のインスタンスを持たせて、そのコンストラクタでここに登録する作りにしていたのだが、
    ///      何故かそのコンストラクタが呼ばれず死んでいる。
    ///      本当はチェックアウト時点でエラーが発生するようなコードにはしたくなかったのだがやむ無し。
    /// </summary>
    private static IEnvironmentData Env = new LocalEnvironmentData();

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
