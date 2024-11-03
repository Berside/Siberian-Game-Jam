using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // Enemy death
        if (gameObject.CompareTag("Enemy"))
        {
            DropWeapon();
            gameObject.layer = 0;
            gameObject.tag = "Untagged";

        }
        // Player death
        else if (gameObject.CompareTag("Player"))
        {

        }
        else
        {
            if (!dead) Invoke("_Death", deathCooldown);
        }
        dead = true;
    }

    private void DropWeapon()
    {
        if (Random.Range(0, 10) > 3)
            return;

        GameObject weapon = Instantiate(gameObject.transform.GetChild(0).gameObject, transform.position + Vector3.one, Quaternion.identity);
        weapon.GetComponent<WeaponStats>().setInteractable(true);
        weapon.AddComponent<PickUpWeapon>();
        weapon.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject.transform.GetChild(0).gameObject);
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
