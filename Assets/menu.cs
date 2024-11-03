using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCTRL : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public GameObject pauseMenu—;

    void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(OnPlayClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    void OnPlayClick()
    {
        Debug.Log("OnPlayClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        pauseMenu—.SetActive(false);
    }

    void OnExitClick()
    {
        Debug.Log("OnExitClick");
        SceneManager.LoadScene("MainMenu");
    }
}
