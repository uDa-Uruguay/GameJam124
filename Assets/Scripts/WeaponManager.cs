using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject[] availableWeapons;
    public int currentWeaponID = 0;
    private int maxID;

    [Header("Current weapon data")]
    private WeaponData _weaponData;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;

    private void OnEnable()
    {
        StartCoroutine(RefreshTime());
        maxID = availableWeapons.Length - 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            availableWeapons[currentWeaponID].SetActive(false);

            if ((currentWeaponID + 1) <= maxID) currentWeaponID += 1;
            else if (currentWeaponID >= maxID) currentWeaponID = 0;

            UpdateWeaponInfo();

            availableWeapons[currentWeaponID].SetActive(true);
        }
    }

    private void UpdateWeaponInfo()
    {
        _weaponData = availableWeapons[currentWeaponID].GetComponent<WeaponData>();
        maxAmmo = _weaponData.maxAmmo;
        currentAmmo = _weaponData.currentAmmo;

        GameEvents.current.WeaponChange(maxAmmo, currentAmmo);
    }

    private IEnumerator RefreshTime()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateWeaponInfo();
        GameEvents.current.WeaponChange(maxAmmo, currentAmmo);
    }
}
