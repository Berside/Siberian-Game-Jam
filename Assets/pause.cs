using UnityEngine;
using System.Collections;
public class PauseMenuController : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().isDead())
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        GetComponent<Canvas>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        GetComponent<Canvas>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}