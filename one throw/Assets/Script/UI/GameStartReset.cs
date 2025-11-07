using UnityEngine;

public class GameStartReset : MonoBehaviour
{
    void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.ResetButtonStates();
            Debug.Log("ゲーム開始時にボタン状態リセットしました");
        }
    }
}
