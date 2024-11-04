using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCTRL : MonoBehaviour
{
    public Button playButton;
    public Button restartButton;
    public Button exitButton;

    void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(GetComponent<PauseMenuController>().ResumeGame);
        restartButton.onClick.AddListener(OnRestartClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnExitClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
