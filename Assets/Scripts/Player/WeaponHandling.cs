using System.Collections.Generic;
using UnityEngine;

public class WeaponHandling : MonoBehaviour
{
    public static WeaponHandling Instance { get; private set; }
    [SerializeField] private Transform weaponParent;
    [SerializeField] private Vector3 weaponOffset;
    private List<Weapon> weapons;
    private int currentWeapon = 0;

    private void Awake()
    {
        //Initialize
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
        if (weapons != null)
        {
            Debug.LogError("weaponsList is not null");
        }
        else weapons = new List<Weapon>();
    }

    private void OnEnable()
    {
        PlayerInput.OnInput1Pressed += ChangeWeapon;
        PlayerInput.OnInput2Pressed += ChangeWeapon;
        PlayerInput.OnInput3Pressed += ChangeWeapon;
        PlayerInput.OnInput4Pressed += ChangeWeapon;
    }
    private void OnDisable()
    {
        PlayerInput.OnInput1Pressed -= ChangeWeapon;
        PlayerInput.OnInput2Pressed -= ChangeWeapon;
        PlayerInput.OnInput3Pressed -= ChangeWeapon;
        PlayerInput.OnInput4Pressed -= ChangeWeapon;
    }

    public void ChangeWeapon(int weaponIndex)
    {
        //If the weapon is not yet unlocked, avoid changing
        if (weaponIndex >= weapons.Count || !weapons[weaponIndex].GetComponent<Weapon>().GetRangedWeaponDataSO().isBought)
        {
            Debug.Log("Weapon not bought yet");
            return;
        }


        //Else change weapon
        weapons[currentWeapon].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeapon = weaponIndex;
        UpdateAmmoUI(weapons[currentWeapon].GetComponent<Weapon>());
    }

    private void UpdateAmmoUI(Weapon weapon)
    {
        WeaponEvents.TriggerOnWeaponSwitch(weapon);
    }

    public Weapon GetActiveWeapon()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.gameObject.activeSelf)
            {
                return weapon.GetComponent<Weapon>();
            }
        }
        return null; // Hvis intet våben er aktivt
    }

    public Transform GetWeaponParent()
    {
        return weaponParent;
    }
    public Vector3 GetWeaponOffset()
    {
        return weaponOffset;
    }

    public void ClearWeaponsList()
    {
        foreach (Weapon weapon in weapons)
        {
            Destroy(weapon.gameObject);
        }
        weapons.Clear();
    }

    public void AddWeaponToWeaponsList(Weapon weapon)
    {
        weapons.Add(weapon);
    }
}
