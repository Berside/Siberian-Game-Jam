using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class  Trigger : MonoBehaviour
{
    [SerializeField]
    protected GameObject Door;

    private bool doorClosed = true;

    private bool PlayerEnteredTriggerZone;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter2DPlayer");
            PlayerEnteredTriggerZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (other.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2DPlayer");
            PlayerEnteredTriggerZone = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("uPDATE2DPlayer");
            if (PlayerEnteredTriggerZone == true)
            {
                if (doorClosed)
                {
                    Door.transform.Rotate(0, 0, -90);
                    doorClosed = false;
                }
                else
                {
                    Door.transform.Rotate(0, 0, 90);
                    doorClosed = true;
                }
            }
        }
    }
}