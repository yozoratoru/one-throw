using UnityEngine;

public class DragAndThrowController : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging = false;
    private bool isClickLocked = false; // 左クリックロック用のフラグ
    private Rigidbody2D rb;

    public float forceMultiplier = 5.0f;
    public GameObject house;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // 最初に物理シミュレーションを無効にする

        if (house == null)
        {
            Debug.LogError("Houseオブジェクトがアサインされていません。");
        }
    }

    void Update()
    {
        HandleDragging();

        // 右クリックが押されたときにシーンをリロードする
        if (Input.GetMouseButtonDown(1)) // 右クリック
        {
            GameManager.Instance.RestartGame();
        }
    }

    private void HandleDragging()
    {
        if (isClickLocked)
        {
            // 左クリックがロックされている場合は何もしない
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = (startPoint - endPoint) * forceMultiplier;
            rb.isKinematic = false; // 物理シミュレーションを有効にする
            rb.AddForce(force, ForceMode2D.Impulse);
            isDragging = false;

            // 左クリックをロックする
            isClickLocked = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == house)
        {
            rb.velocity = Vector2.zero; // 残っている動きを停止する
            rb.angularVelocity = 0f; // 回転も停止する

            // 家のスプライトを非表示にする
            house.GetComponent<SpriteRenderer>().enabled = false;

            // GameManagerに通知する
            GameManager.Instance.OnGameClear();

            // AudioManager.Instance.PlayExplosionSound();
        }
    }
}
