using UnityEngine;

public class SceneIntro : MonoBehaviour
{
    public Animator textAnimator; // TextオブジェクトにアタッチされたAnimator
    public float delayBeforeAnimation = 3f;  // アニメーション開始までの遅延時間

    private bool animationStarted = false; // アニメーションが開始されたかどうかを追跡するフラグ

    void Start()
    {
        // 3秒後にアニメーションを開始
        Invoke("StartTextAnimation", delayBeforeAnimation);
    }

    void StartTextAnimation()
    {
        // アニメーションが1回だけ開始されるようにフラグでチェック
        if (!animationStarted)
        {
            animationStarted = true; // アニメーション開始フラグをセット
            textAnimator.SetTrigger("SlideIn"); // アニメーションを再生
        }
    }
}
