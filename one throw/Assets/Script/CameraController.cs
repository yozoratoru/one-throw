using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    public Camera mainCamera;
    public float zoomSize = 3f;       // ズーム後のOrthographic Size
    public float zoomDuration = 1f;   // ズームアニメーションの時間
    public float moveDistanceX = 3f;  // 横移動距離
    public float moveDistanceY = 2f;  // 縦移動距離
    public float moveDuration = 1f;   // 移動時間（秒）

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 重複防止
        }
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // カメラをOrthographicに設定（これ重要）
        mainCamera.orthographic = true;
    }

    // ズームと横移動を同時に行う
    public void ZoomInOnClear()
    {
        // DOTweenでOrthographic Sizeをズームサイズまでアニメーションさせる
        // DOTweenでOrthographic Sizeをズームサイズまでアニメーションさせる
        mainCamera.DOOrthoSize(zoomSize, zoomDuration).SetEase(Ease.InOutQuad);
         // transform.position のx座標をmoveDistanceだけ増やして移動
        // transform.position のx座標をmoveDistanceだけ増やして移動
        transform.DOMoveX(transform.position.x + moveDistanceX, moveDuration)
                 .SetEase(Ease.Linear);
    }

    // 上に移動
    public void MoveUp()
    {
        transform.DOMoveY(transform.position.y + moveDistanceY, moveDuration)
                .SetEase(Ease.Linear);
    }

    // 下に移動
    public void MoveDown()
    {
        transform.DOMoveY(transform.position.y - moveDistanceY, moveDuration)
                .SetEase(Ease.Linear);
    }


}
