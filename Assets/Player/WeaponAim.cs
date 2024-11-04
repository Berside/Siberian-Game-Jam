using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponAim : MonoBehaviour
{
    private Transform aimTransform;

    private Transform gunEndPointPosition;
    private float time = 0;

    private WeaponStats weaponStats;

    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = transform.Find("Aim");
        gunEndPointPosition = aimTransform.GetChild(0).Find("GunEndPointPosition").transform;
        weaponStats = aimTransform.GetChild(0).GetComponent<WeaponStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().isDead())
            return;

        if (gunEndPointPosition == null || weaponStats == null)
        {
            gunEndPointPosition = aimTransform.GetChild(0).Find("GunEndPointPosition").transform;
            weaponStats = aimTransform.GetChild(0).GetComponent<WeaponStats>();
        }

        time += Time.deltaTime;

        HandleAiming();
        HandleShooting();
        HandleReload();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 93f);
    }

    private void HandleShooting()
    {
        if (weaponStats.FullAuto())
        { 
            if (Input.GetButton("Fire1") && weaponStats.getCurrentAmmo() > 0)
            {
                if (time >= weaponStats.ShootCooldown())
                {
                    // here happens shooting :)
                    weaponStats.fire(gunEndPointPosition);
                    time = 0;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && weaponStats.getCurrentAmmo() > 0)
            {
                // here happens shooting :)
                weaponStats.fire(gunEndPointPosition);
            }
        }
    }

    private void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponStats.reload();
        }
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }


}
