using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnChangeSceneButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    void OnChangeSceneButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
