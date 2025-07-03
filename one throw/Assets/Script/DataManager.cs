using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; // Singletonインスタンス

    [HideInInspector] public SaveData data; // json変換するデータのクラス
    string filepath; // jsonファイルのパス
    string fileName = "Data.json"; // jsonファイル名

    private void Awake()
    {
        // Singletonパターンの設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを切り替えても破棄しない
        }
        else
        {
            Destroy(gameObject);
        }

        // パス名取得
        filepath = Application.dataPath + "/" + fileName;

        // ファイルがないとき、初期データでファイル作成
        if (!File.Exists(filepath))
        {
            data = new SaveData();
            data.TotalCoins = 100; // 初期値
            Save(data);
        }
        else
        {
            data = Load(filepath); // ファイルを読み込んでdataに格納
        }
    }

    // jsonとしてデータを保存
    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data); // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false); // ファイル書き込み指定
        wr.WriteLine(json); // json変換した情報を書き込み
        wr.Close(); // ファイル閉じる
    }

    // jsonファイル読み込み
    public SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path); // ファイル読み込み指定
        string json = rd.ReadToEnd(); // ファイル内容全て読み込む
        rd.Close(); // ファイル閉じる

        return JsonUtility.FromJson<SaveData>(json); // jsonファイルを型に戻して返す
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Save(data); // データ保存
        }
    }
    public void ResetButtonStates()
{
    if (data != null)
    {
        data.buttonStates.Clear();
        Save(data);
    }
}

}