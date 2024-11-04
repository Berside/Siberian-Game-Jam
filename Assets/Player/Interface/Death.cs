using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public GameObject RestartWindowBtns;
    public GameObject ContinueWindowBtns;
    public Button RestartBtn;
    public Button ExitBtn;

    public Button ContinueBtn;
    public Button RestartInContinueBtn;

    public Text ScoreText;

    public int killsToContinue;

    private GameObject player;

    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        RestartBtn.onClick.AddListener(Restart);
        ExitBtn.onClick.AddListener(ExitToMainMenu);

        ContinueBtn.onClick.AddListener(Continue);
        RestartInContinueBtn.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Health>().isDead() && !playerDead)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            ScoreText.text = "Очки: " + player.GetComponent<PlayerData>().getScore();

            if (player.GetComponent<PlayerData>().getKills() >= killsToContinue)
            {
                ContinueWindowBtns.SetActive(true);
            }
            else
            {
                RestartWindowBtns.SetActive(true);
            }

            playerDead = true;
        }
    }

    

    public void Restart()
    {
        SceneManager.LoadScene("level1");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Epilogue");
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
