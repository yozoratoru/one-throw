using UnityEngine;

// このスクリプトには LineRenderer コンポーネントが必須
[RequireComponent(typeof(LineRenderer))]
public class DragAndThrowController : MonoBehaviour
{
    // ドラッグの開始点・終了点
    private Vector2 startPoint;
    private Vector2 endPoint;

    private bool isDragging = false;     // 現在ドラッグ中かどうか
    private bool isClickLocked = false;  // 投げた後にクリックを受け付けないためのロック

    private Rigidbody2D rb;              // 物理挙動のためのリジッドボディ

    public float forceMultiplier = 5.0f; // 投げる力の倍率
    public GameObject house;            // 衝突判定対象の家オブジェクト

    private LineRenderer lineRenderer;  // ガイドライン描画用

    void Start()
    {
        // Rigidbody2D を取得
        rb = GetComponent<Rigidbody2D>();

        // 最初は物理挙動を止めておく
        rb.isKinematic = true;

        // house が設定されていなければ警告を出す
        if (house == null)
        {
            Debug.LogError("Houseオブジェクトがアサインされていません。");
        }

        // LineRenderer を取得して初期設定
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;      // 始点と終点の2点
        lineRenderer.enabled = false;        // 最初は非表示にしておく
        // 高速移動による貫通防止
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        HandleDragging(); // マウス操作でドラッグ処理を行う

        // 右クリックでゲームをリスタート
        if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            GameManager.Instance.RestartGame();
        }
    }

    // マウスによるドラッグ・投げ処理
    private void HandleDragging()
    {
        // 一度投げたらロックして、再び投げられないようにする
        if (isClickLocked)
        {
            return;
        }

        // 左クリックを押した瞬間にドラッグ開始
        if (Input.GetMouseButtonDown(0))
        {
            // スクリーン座標 → ワールド座標に変換
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            lineRenderer.enabled = true; // ガイドラインを表示
        }

        // 左クリックを押し続けている間、ガイドラインを更新
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ガイドラインの始点をプレイヤーの位置に
            lineRenderer.SetPosition(0, transform.position);

            // 力のベクトルを計算し、終点をその方向へ延ばす（少し縮小して視認しやすく）
            Vector2 force = (startPoint - currentPoint) * forceMultiplier * 0.1f;
            lineRenderer.SetPosition(1, (Vector2)transform.position + force);
        }

        // 左クリックを離したら、実際に投げる
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ドラッグ方向のベクトルを力として使う
            Vector2 force = (startPoint - endPoint) * forceMultiplier;

            // 物理挙動を有効にして、力を加える
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode2D.Impulse);

            isDragging = false;
            isClickLocked = true;         // 再度投げられないようにロック
            lineRenderer.enabled = false; // ガイドラインを非表示に
        }
    }

    // 家とぶつかったときの処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突対象が house オブジェクトであれば
        if (collision.gameObject == house)
        {
            // 動きを完全に停止（速度・回転をゼロに）
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // 家の見た目を消す
            house.GetComponent<SpriteRenderer>().enabled = false;

            // ゲームクリア処理を呼び出す
            GameManager.Instance.OnGameClear();
        }
    }
}
