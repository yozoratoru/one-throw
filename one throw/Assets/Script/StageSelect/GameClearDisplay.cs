using UnityEngine;
using UnityEngine.UI;

public class GameClearDisplay : MonoBehaviour
{
    public GameObject gameClearPanel; // Panel containing the game clear text and background

    void Start()
    {
        gameClearPanel.SetActive(false); // Hide the panel at the start of the game
    }

    public void ShowGameClear()
    {
        gameClearPanel.SetActive(true); // Show the panel when the game is cleared
    }
}
