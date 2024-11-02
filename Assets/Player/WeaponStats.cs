using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField]
    protected float damage = 1;

    [SerializeField]
    protected int totalAmmo = 45;

    [SerializeField]
    protected int magSize = 15;

    [SerializeField]
    protected float reloadCooldown = 1.5f;

    [SerializeField]
    protected float shootCooldown = .1f;

    [SerializeField]
    protected bool fullAuto = false;

    [SerializeField]
    private int totalAmmoLeft;

    [SerializeField]
    private int magCurrentAmmo;

    [SerializeField]
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        totalAmmoLeft = totalAmmo;
        magCurrentAmmo = 0;
    }

    public int getTotalAmmo()
    {
        return totalAmmo;
    }
    public int getCurrentAmmo()
    {
        return magCurrentAmmo;
    }
    public bool FullAuto()
    {
        return fullAuto;
    }
    public float getDamage()
    {
        return damage;
    }
    public float ShootCooldown()
    {
        return shootCooldown;
    }
    public float getReloadCooldown()
    {
        return reloadCooldown;
    }

    public void fire()
    {
        if (magCurrentAmmo > 0)
        {
            magCurrentAmmo--;
        }
    }

    public void reload()
    {
        if (magCurrentAmmo != magSize)
        {
            if (!reloading)
                Invoke("_reload", reloadCooldown);
            reloading = true;
        }
    }

    private void _reload()
    {
        if (totalAmmoLeft >= magSize - magCurrentAmmo && magCurrentAmmo < magSize)
        {
            totalAmmoLeft -= (magSize - magCurrentAmmo);
            magCurrentAmmo = magSize;
        }
        else
        {
            magCurrentAmmo += totalAmmoLeft;
            totalAmmoLeft = 0;
        }

        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
