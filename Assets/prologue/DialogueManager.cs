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

    public Image Cop;
    public Image Bro;

    private Tuple<string, string>[] messages = {
        new Tuple<string, string>("Cop", "�� ����� ������������ ���� ��������!?"),
        new Tuple<string, string>("Bro", "��� �� ���� ����"),
        new Tuple<string, string>("Bro", "*����������*"),
        new Tuple<string, string>("Bro", "��� �� ����!?"),
        new Tuple<string, string>("Bro", "����������������"),
        new Tuple<string, string>("Bro", "*�������� �� ����*"),
        new Tuple<string, string>("Cop", "��� � �����? ��� �� �������?"),
        new Tuple<string, string>("Bro", "������ �*****� �������� �������"),
        new Tuple<string, string>("Bro", "���� ��� �� ������(((((("),
        new Tuple<string, string>("Cop", "���������� �� ������!!!"),
        new Tuple<string, string>("Bro", "��"),
        new Tuple<string, string>("Bro", ""),
        new Tuple<string, string>("Cop", "���� ��������� � ���� ��� � ��������!"),
        new Tuple<string, string>("Cop", "*����� ����� � ������� ����� �������*"),
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

        if (currentMessage.Item1 == "Cop")
        {
            Bro.enabled = false;
            Cop.enabled = true;
            textArea.transform.localPosition = new Vector3(283, transform.position.y, transform.position.z);
        }
        else
        {
            if (currentMessage.Item2 == "")
            {
                Bro.sprite = Resources.Load("Sprites/DeadBro", typeof(Sprite)) as Sprite;
            }
            Bro.enabled = true;
            Cop.enabled = false;
            textArea.transform.localPosition = new Vector3(-214, transform.position.y, transform.position.z);
        }

        textArea.text = currentMessage.Item2;

        if (Input.GetButtonDown("Fire1"))
        {
            message++;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("level1");
        }
    }
}
