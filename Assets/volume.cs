using UnityEngine;
using System.Collections;

public class GlobalVolumeManager : MonoBehaviour
{
    public static GlobalVolumeManager Instance { get; private set; }

    [SerializeField] private float defaultVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Загрузить сохраненный уровень громкости при начале новой сцены
        LoadSavedVolume();
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
    }

    private void LoadSavedVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        AudioListener.volume = Mathf.Clamp01(savedVolume);
    }
}
