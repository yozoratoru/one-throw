using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StageSelectManager : MonoBehaviour
{
    public Image blackBackground;  // 黒背景のImage
    public Animator blackBackgroundAnimator;  // 黒背景のアニメーター
    private bool isTransitioning = false;

    // この関数はボタンのOnClick()イベントに設定します。
    public void SelectStage(int stageNumber)
    {
        // すでに移動中であれば何もしない
        if (isTransitioning) return;

        // シーン遷移開始
        StartCoroutine(TransitionToScene(stageNumber));
    }

    private IEnumerator TransitionToScene(int stageNumber)
    {
        // アニメーション開始
        isTransitioning = true;
        blackBackgroundAnimator.SetTrigger("StartSlideIn");

        // 黒背景が表示される時間を延ばす (例: 3秒に変更)
        yield return new WaitForSeconds(3.0f); // アニメーションの長さに合わせて調整

        // シーン移動
        string sceneName = "Stage" + stageNumber.ToString();
        SceneManager.LoadScene(sceneName);

        // シーンロード後にスライドアウトを開始するために少し待機（スライドアウト部分は省略）
        yield return new WaitForSeconds(1f); // 必要に応じて調整

        // 移動完了
        isTransitioning = false;
    }

    // AnimationEventで呼ばれる関数を追加
    public void OnSlideInComplete()
    {
        // スライドインが完了した後に呼ばれる
        Debug.Log("スライドイン完了!");
        // 必要に応じて処理を追加
    }
}
