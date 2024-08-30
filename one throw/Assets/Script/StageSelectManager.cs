using UnityEngine;
using UnityEngine.SceneManagement; // シーンのロードに必要

public class StageSelectManager : MonoBehaviour
{
    // この関数はボタンのOnClick()イベントに設定します。
    public void SelectStage(int stageNumber)
    {
        // ステージ番号に対応するシーン名を設定（例: Stage1, Stage2, Stage3）
        string sceneName = "Stage" + stageNumber.ToString();
        
        // 指定されたシーンに移動
        SceneManager.LoadScene(sceneName);
    }
}
