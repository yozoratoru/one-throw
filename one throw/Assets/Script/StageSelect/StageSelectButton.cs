using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    // ボタンから設定するステージ番号（インスペクターで設定可能）
    public int stageNumber;

    // ボタンのOnClickイベントにこの関数を登録する
    public void LoadStage()
    {
        string sceneName = "Stage" + stageNumber;
        Debug.Log("Loading scene: " + sceneName);
        
        // シーンがビルドに登録されているかどうか確認（任意）
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " is not found in Build Settings!");
        }
    }
}
