using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponAim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> onShoot;
    public class OnShootEventArgs: EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;

    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // here happens shooting :)

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
