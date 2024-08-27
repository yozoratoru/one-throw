using UnityEngine;
using UnityEngine.UI;  // UIを操作するために必要

public class MenuController : MonoBehaviour
{
    // MenuManagerへの参照
    public MenuManager menuManager;

    // ボタンがクリックされたときに呼び出されるメソッド
    public void OnMenuButtonClicked()
    {
        if (menuManager != null)
        {
            menuManager.ToggleMenu();
        }
    }
}
