using System;

public static class WeaponEvents
{
    public static event Action<RangedWeapon> OnWeaponShoot;
    public static event Action<RangedWeapon> OnWeaponReload;
    public static event Action<Weapon> OnWeaponSwitch;
    public static void TriggerOnWeaponShoot(RangedWeapon weapon)
    {
        OnWeaponShoot?.Invoke(weapon);
    }
    public static void TriggerOnWeaponReload(RangedWeapon weapon)
    {
        OnWeaponReload?.Invoke(weapon);
    }
    public static void TriggerOnWeaponSwitch(Weapon weapon)
    {
        OnWeaponSwitch?.Invoke(weapon);
    }
}
