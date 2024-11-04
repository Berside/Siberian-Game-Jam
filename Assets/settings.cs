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
        // ��������� ������� AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource �� ������. ����������, ���������� AudioSource � ������� ����.");
            return;
        }

        LoadSettings();
    }

    void Update()
    {
        // ���������� ��������� ������ ���� AudioSource ��������
        if (audioSource != null && volumeSlider != null)
        {
            float newVolume = Mathf.Clamp(volumeSlider.value, 0f, 1f);
            audioSource.volume = newVolume;
            PlayerPrefs.SetFloat(VolumePreferenceKey, newVolume);
        }

        // ���������� �������������� ������ ������ ���� Toggle ������������
        if (fullscreenToggle != null)
        {
            UpdateFullscreen();
        }
    }

    // ����� ��� ��������� ���������
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat(VolumePreferenceKey, volume);
        }
        else
        {
            Debug.LogWarning("AudioSource �� ������. �� ���� ���������� ���������.");
        }
    }

    // ����� ��� ��������� �������� �������� ���������
    public float GetVolume()
    {
        if (audioSource != null)
        {
            return audioSource.volume;
        }
        return 1f; // �������� �� ���������, ���� AudioSource �� ������
    }

    // ����� ��� �������� ��������
    private void LoadSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePreferenceKey, 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt(FullscreenPreferenceKey, 0) == 1;

        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }
    }

    // ����� ��� ���������� �������� �������������� ������
    private void UpdateFullscreen()
    {
        if (fullscreenToggle != null)
        {
            Screen.fullScreenMode = fullscreenToggle.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
            PlayerPrefs.SetInt(FullscreenPreferenceKey, fullscreenToggle.isOn ? 1 : 0);
        }
    }
}
