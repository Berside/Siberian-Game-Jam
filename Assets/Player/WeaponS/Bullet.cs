using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float damage;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health objectHealth = other.GetComponent<Health>();

        if (objectHealth != null && (other.CompareTag("Enemy") || other.CompareTag("Player")) && !objectHealth.isDead())
        {
            objectHealth.takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
