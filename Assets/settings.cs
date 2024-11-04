using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public AudioSource audioSource;

    private const string VolumePreferenceKey = "preferred_volume";
    private const string FullscreenPreferenceKey = "fullscreen";

    void Start()
    {
        // Проверяем наличие AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource не найден. Пожалуйста, прикрепите AudioSource к объекту игры.");
            return;
        }

        LoadSettings();
    }

    void Update()
    {
        // Обновление громкости только если AudioSource доступен
        if (audioSource != null && volumeSlider != null)
        {
            float newVolume = Mathf.Clamp(volumeSlider.value, 0f, 1f);
            audioSource.volume = newVolume;
            PlayerPrefs.SetFloat(VolumePreferenceKey, newVolume);
        }

        // Обновление полноэкранного режима только если Toggle присутствует
        if (fullscreenToggle != null)
        {
            UpdateFullscreen();
        }
    }

    // Метод для установки громкости
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat(VolumePreferenceKey, volume);
        }
        else
        {
            Debug.LogWarning("AudioSource не найден. Не могу установить громкость.");
        }
    }

    // Метод для получения текущего значения громкости
    public float GetVolume()
    {
        if (audioSource != null)
        {
            return audioSource.volume;
        }
        return 1f; // Значение по умолчанию, если AudioSource не найден
    }

    // Метод для загрузки настроек
    private void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePreferenceKey, 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt(FullscreenPreferenceKey, 0) == 1;

        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }
    }

    // Метод для обновления значения полноэкранного режима
    private void UpdateFullscreen()
    {
        if (fullscreenToggle != null)
        {
            Screen.fullScreenMode = fullscreenToggle.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
            PlayerPrefs.SetInt(FullscreenPreferenceKey, fullscreenToggle.isOn ? 1 : 0);
        }
    }
}
