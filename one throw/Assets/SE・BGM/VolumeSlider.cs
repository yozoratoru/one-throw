using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    [Header("スライダー参照")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private const string BGM_VOLUME_KEY = "BGM_VOLUME";
    private const string SE_VOLUME_KEY = "SE_VOLUME";

    void Start()
    {
        // 保存された音量を読み込んで反映（なければデフォルト0.5f）
        float savedBGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 0.5f);
        float savedSEVolume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, 1.0f);

        bgmSlider.value = savedBGMVolume;
        seSlider.value = savedSEVolume;

        // サウンドマネージャーにも反映
        SoundManager.instance.SetBGMVolume(savedBGMVolume);
        SoundManager.instance.SetSEVolume(savedSEVolume);

        // スライダーのリスナー登録
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    public void SetBGMVolume(float volume)
    {
        SoundManager.instance.SetBGMVolume(volume);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    public void SetSEVolume(float volume)
    {
        SoundManager.instance.SetSEVolume(volume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
}
