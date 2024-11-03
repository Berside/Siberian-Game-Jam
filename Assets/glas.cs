using UnityEngine;

public class BulletTriggerDeactivation : MonoBehaviour
{
    public GameObject objectToDeactivate; // Объект, который будет деактивирован
    private bool PlayerEnteredTriggerZone;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            objectToDeactivate.SetActive(false);
        }
    }
}
