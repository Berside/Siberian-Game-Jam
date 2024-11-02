using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;

    [SerializeField]
    protected float currentHealth;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            Death();
        }
    }

    public void Death(float deathCooldown = 0f)
    {
        // То что мертво, умереть не может
        if (!dead) Invoke("_Death", deathCooldown);
        dead = true;
    }

    private void _Death()
    {
        if (isDead())
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(float damage)
    {
        if (currentHealth - damage >= 0) currentHealth -= damage;
        else currentHealth = 0;
    }

    public bool isDead()
    {
        return currentHealth == 0;
    }
}
