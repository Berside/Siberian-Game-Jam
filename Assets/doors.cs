using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class  Trigger : MonoBehaviour
{

    public GameObject disableObject;
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
                Debug.Log("BAB");
                disableObject.SetActive(false);
            }
        }
    }
}