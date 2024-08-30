using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameClearDisplay gameClearDisplay; // Reference to the GameClearDisplay

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnGameClear()
    {
        // Delay the Game Clear text display by 1 second
        Invoke("ShowGameClearWithDelay", 1f);

        // Load the SampleScene after 3 seconds
        Invoke("LoadSampleScene", 3f);
    }

    private void ShowGameClearWithDelay()
    {
        // Display the Game Clear message
        gameClearDisplay.ShowGameClear();
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadSampleScene()
    {
        // Load the SampleScene
        SceneManager.LoadScene("Stage Select");
    }

}
