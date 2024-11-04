using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioMan : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        GlobalVolumeManager.Instance.SetMasterVolume(GetVolume());
    }

    void Update()
    {
        if (volumeSlider != null)
        {
            float newVolume = Mathf.Clamp(volumeSlider.value, 0f, 1f);
            GlobalVolumeManager.Instance.SetMasterVolume(newVolume);
        }
    }

    // Метод для установки громкости теперь использует GlobalVolumeManager
    public void SetVolume(float volume)
    {
        GlobalVolumeManager.Instance.SetMasterVolume(Mathf.Clamp01(volume));
    }

    // Метод для получения текущего значения громкости
    public float GetVolume()
    {
        return GlobalVolumeManager.Instance.GetMasterVolume();
    }
}
