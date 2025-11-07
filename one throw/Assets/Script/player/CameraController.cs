using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform player;        // プレイヤーをInspectorで設定
    public float zoomSize = 2f;     // ズーム後のサイズ
    public float duration = 1.5f;   // アニメーション時間
    public static CameraController Instance { get; private set; }
    void Awake()
    {
        // シングルトン初期化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PayerInCamera()
    {
        // カメラをズーム＆移動（同時に）
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10f); // カメラはZ = -10固定

        // 並行に実行
        Camera.main.DOOrthoSize(zoomSize, duration).SetEase(Ease.InOutSine);
        Camera.main.transform.DOMove(targetPos, duration).SetEase(Ease.InOutSine);
    }
}
