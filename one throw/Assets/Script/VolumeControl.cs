using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider bgmSlider;  // BGM用スライダー
    public Slider seSlider;   // SE用スライダー

    private void Start()
    {
        // スライダーの初期値をAudio Sourceのボリュームに合わせる
        if (VolumeManager.Instance != null)
        {
            bgmSlider.value = VolumeManager.Instance.BGMVolume;
            seSlider.value = VolumeManager.Instance.SEVolume;
        }

        // スライダーのリスナーを設定
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
    }

    // BGMのボリュームが変更されたときに呼ばれるメソッド
    private void OnBGMVolumeChanged(float volume)
    {
        if (VolumeManager.Instance != null)
        {
            VolumeManager.Instance.BGMVolume = volume;
        }
    }

    // SEのボリュームが変更されたときに呼ばれるメソッド
    private void OnSEVolumeChanged(float volume)
    {
        if (VolumeManager.Instance != null)
        {
            VolumeManager.Instance.SEVolume = volume;
        }
    }
}
