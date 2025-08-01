using UnityEngine;

public class LoopSEStarter : MonoBehaviour
{
    [SerializeField] private string loopSEName = "LoopClip"; // ループ再生したいSE名（例: "EngineLoop"）

    void Start()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayLoopingSE(loopSEName);
        }
    }
}
