using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // メニューのパネルを参照するための変数
    public GameObject menuPanel;

    // メニューが開いているかどうかのフラグ
    private bool isMenuOpen = false;

    void Start()
    {
        // 最初にメニューを非表示に設定
        menuPanel.SetActive(false);
    }

    // メニューの表示状態を切り替えるメソッド
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);

        // メニューが開いているときはゲームを一時停止する
        if (isMenuOpen)
        {
            Time.timeScale = 0f; // ゲームを一時停止
        }
        else
        {
            Time.timeScale = 1f; // ゲームを再開
        }
    }

    // メニューを開いているかどうかを取得するメソッド
    public bool IsMenuOpen()
    {
        return isMenuOpen;
    }
}
