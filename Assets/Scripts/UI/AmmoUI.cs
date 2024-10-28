using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Image ammoImage;
    private Weapon activeWeapon;
    private bool isReloading;
    private float reloadTime;
    private float reloadStartTime;
    private float targetFill;
    private float startFill;

    private void Awake()
    {
        isReloading = false;
    }

    private void Update()
    {
        if (isReloading)
        {
            ReloadTimer();
        }
    }
    private void OnEnable()
    {
        WeaponEvents.OnWeaponShoot += WeaponEvents_OnWeaponShoot;
        WeaponEvents.OnWeaponSwitch += WeaponEvents_OnWeaponSwitch;
        WeaponEvents.OnWeaponReload += WeaponEvents_OnWeaponReload;
    }

    private void OnDisable()
    {
        WeaponEvents.OnWeaponShoot -= WeaponEvents_OnWeaponShoot;
        WeaponEvents.OnWeaponSwitch -= WeaponEvents_OnWeaponSwitch;
        WeaponEvents.OnWeaponReload -= WeaponEvents_OnWeaponReload;
    }

    private void WeaponEvents_OnWeaponReload(RangedWeapon weapon)
    {
        ResetReloadStats();
        isReloading = true;
    }

    private void WeaponEvents_OnWeaponSwitch(Weapon weapon)
    {
        activeWeapon = weapon;
        UpdateAmmoUI();
    }


    private void WeaponEvents_OnWeaponShoot(RangedWeapon weapon)
    {
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (activeWeapon != null && activeWeapon.TryGetComponent(out RangedWeapon rangedWeapon))
        {
            int currentAmmo = rangedWeapon.GetCurrentAmmoCount();
            //Debug.Log("Current ammo: " + currentAmmo);
            int maxAmmo = rangedWeapon.GetRangedWeaponDataSO().maxAmmo;

            float ammoPercentage = (float)currentAmmo / maxAmmo;
            ammoImage.fillAmount = ammoPercentage;
            //Debug.Log(ammoPercentage);
        }
        else
        {
            // Inactive weapon or weapon without RangedWeapon component, updates fillAmount to 0
            ammoImage.fillAmount = 0f;
        }
    }

    private void ReloadTimer()
    {
        float elapsedTime = Time.time - reloadStartTime;

        float time = Mathf.Clamp01(elapsedTime / reloadTime);

        ammoImage.fillAmount = Mathf.Lerp(startFill, targetFill, time);

        if (time >= 1f)
        {
            //Once done, make sure ammoImage fillamout = targetFill
            ammoImage.fillAmount = targetFill;
            isReloading = false;
        }
    }

    private void ResetReloadStats()
    {
        reloadStartTime = Time.time;
        targetFill = 1f;
        startFill = ammoImage.fillAmount;
        reloadTime = activeWeapon.GetRangedWeaponDataSO().reloadTime;
    }
}
