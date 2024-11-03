using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAmmo : MonoBehaviour
{
    [SerializeField]
    protected Canvas ammoInterface;

    private Text reloadText;

    private WeaponStats weaponStats;
    // Start is called before the first frame update
    void Start()
    {
        weaponStats = GameObject.FindWithTag("Player").transform.Find("Aim").transform.GetChild(0).transform.GetComponent<WeaponStats>();
        reloadText = ammoInterface.transform.Find("Reload").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponStats == null)
        {
            weaponStats = GameObject.FindWithTag("Player").transform.Find("Aim").transform.GetChild(0).transform.GetComponent<WeaponStats>();
        }

        if (weaponStats.getCurrentAmmo() <= 3 && weaponStats.getTotalAmmoLeft() != 0)
        {
            reloadText.enabled = true;
        }
        else
        {
            reloadText.enabled = false;
        }
        GetComponent<Text>().text = weaponStats.getCurrentAmmo().ToString() + "/" +
            weaponStats.getMagSize().ToString() + " " + weaponStats.getTotalAmmoLeft().ToString();
    }
}
