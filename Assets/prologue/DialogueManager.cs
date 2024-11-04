using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text textArea;

    public Text charName;

    public Image Cop;
    public Image Bro;

    private Tuple<string, string>[] messages = {
        new Tuple<string, string>("Рик", "Ты снова упариваешься этой наркотой!?"),
        new Tuple<string, string>("Сауль", "Это не твое дело"),
        new Tuple<string, string>("Сауль", "*занюхивает*"),
        new Tuple<string, string>("Сауль", "Что со мной!?"),
        new Tuple<string, string>("Сауль", "АААААААААААААААА"),
        new Tuple<string, string>("Сауль", "*Корчится от боли*"),
        new Tuple<string, string>("Рик", "Что с тобой? Что ты вынюхал?"),
        new Tuple<string, string>("Сауль", "Барыга п*****с подсунул метадон"),
        new Tuple<string, string>("Сауль", "Меня уже не спасти(((((("),
        new Tuple<string, string>("Рик", "Онееееееет не умирай!!!"),
        new Tuple<string, string>("Сауль", "……"),
        new Tuple<string, string>("Сауль", ""),
        new Tuple<string, string>("Рик", "Пора покончить с этим раз и навсегда!"),
        new Tuple<string, string>("Рик", "*Звуки шагов в сторону нарко притона*"),
    };

    private int message = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (message == messages.Length)
        {
            SceneManager.LoadScene("level1");
        }

        var currentMessage = messages[message];

        if (currentMessage.Item1 == "Рик")
        {
            Bro.enabled = false;
            Cop.enabled = true;
            charName.transform.localPosition = new Vector3(-396, charName.transform.localPosition.y, charName.transform.localPosition.z);
            textArea.transform.localPosition = new Vector3(283, textArea.transform.localPosition.y, textArea.transform.localPosition.z);
        }
        else
        {
            if (currentMessage.Item2 == "")
            {
                Bro.sprite = Resources.Load("Sprites/DeadBro", typeof(Sprite)) as Sprite;
            }
            Bro.enabled = true;
            Cop.enabled = false;
            charName.transform.localPosition = new Vector3(493, charName.transform.localPosition.y, charName.transform.localPosition.z);
            textArea.transform.localPosition = new Vector3(-214, textArea.transform.localPosition.y, textArea.transform.localPosition.z);
        }

        charName.text = currentMessage.Item1;
        textArea.text = currentMessage.Item2;

        if (Input.GetButtonDown("Fire1"))
        {
            message++;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (message > 0)
                message--;
            else
                message = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("level1");
        }
    }
}
