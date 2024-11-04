using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected float bulletSpeed;

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
    private bool interactable = false;

    [SerializeField]
    private int totalAmmoLeft;

    [SerializeField]
    private int magCurrentAmmo;

    [SerializeField]
    private bool reloading = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        totalAmmoLeft = totalAmmo;
        magCurrentAmmo = magSize;

        animator = GetComponent<Animator>();
    }

    public bool getInteractable()
    {
        return interactable;
    }
    public void setInteractable(bool value)
    {
        interactable = value;
    }

    public int getTotalAmmo()
    {
        return totalAmmo;
    }
    public int getCurrentAmmo()
    {
        return magCurrentAmmo;
    }
    public int getMagSize()
    {
        return magSize;
    }
    public bool FullAuto()
    {
        return fullAuto;
    }
    public float ShootCooldown()
    {
        return shootCooldown;
    }
    public int getTotalAmmoLeft()
    {
        return totalAmmoLeft;
    }
    public float getReloadCooldown()
    {
        return reloadCooldown;
    }

    public void fire(Transform gunEndPointPosition)
    {
        if (magCurrentAmmo > 0)
        {
            animator.SetTrigger("Fire");

            GameObject bullet = Instantiate(bulletPrefab, gunEndPointPosition.position, gunEndPointPosition.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(-gunEndPointPosition.up * bulletSpeed, ForceMode2D.Impulse);

            magCurrentAmmo--;
        }
    }

    public void reload()
    {
        if (magCurrentAmmo != magSize)
        {
            if (!reloading)
            {
                animator.SetTrigger("Reload");
                Invoke("_reload", reloadCooldown);
            }
            reloading = true;
        }
    }
    public bool Reloading()
    {
        return reloading;
    }
    public void setReloading(bool value)
    {
        reloading = value;
    }

    private void _reload()
    {
        if (totalAmmoLeft >= (magSize - magCurrentAmmo) && magCurrentAmmo < magSize)
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
