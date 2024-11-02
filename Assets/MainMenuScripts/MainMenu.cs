using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
    public GameObject mainMenuPanel;
    public Text buttonTextComponent;
    public Toggle toggle;
    public Slider slider;

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
        SceneManager.LoadScene("level1");
    }
    void OnExitClick()
    {
        Application.Quit(); 
    }
    void OnSettingsClick()
    {
        mainMenuPanel.SetActive(false);
    }
}