using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBullet : MonoBehaviour
{
    private Bullet bulletInfo;
    private WeaponData _weaponInfo;
    private WeaponManager _weaponManager;

    private bool correctWeapon = false;

    private void Start()
    {
        bulletInfo = transform.parent.GetComponent<Bullet>();
        if (bulletInfo) _weaponInfo = bulletInfo.weaponInfo;

        _weaponManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponManager>();
    }

    private void Update()
    {
        if (!_weaponManager) return;
        if (_weaponManager.currentWeaponID == 0) correctWeapon = true;
        else correctWeapon = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!correctWeapon) return;

        if (collision.gameObject.tag == "Player" && bulletInfo.isCollectable)
        {
            if (_weaponInfo.maxAmmo > _weaponInfo.currentAmmo)
            {
                _weaponInfo.currentAmmo += 1;
                Destroy(transform.parent.gameObject);
                GameEvents.current.AmmoCollected();
                GameEvents.current.WeaponChange(_weaponInfo.maxAmmo, _weaponInfo.currentAmmo);
            }
        }
    }
}
