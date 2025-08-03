using UnityEngine;

public class SEOnHomeCollision : MonoBehaviour
{
    [SerializeField] private string hitSEName = "HitClip"; // インスタンスされたときに鳴らすSEの名前

    private void Start()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySE(hitSEName);
        }
    }
}
