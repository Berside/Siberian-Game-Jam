using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private bool PlayerEnteredTriggerZone;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
            if (PlayerEnteredTriggerZone == true)
            {
                Destroy(gameObject.GetComponent<PickUpWeapon>());
                Destroy(player.transform.Find("Aim").GetChild(0).gameObject);
                var weapon = Instantiate(gameObject, transform.position, Quaternion.identity);
                weapon.transform.SetParent(player.transform.Find("Aim"));
                weapon.transform.localPosition = new Vector3(-0.2499f, -0.573f, 0);
                weapon.transform.localRotation = Quaternion.identity;
                weapon.GetComponent<WeaponStats>().setReloading(false);
                weapon.GetComponent<BoxCollider2D>().enabled = false;

                Destroy(gameObject);
            }
        }
    }
}
