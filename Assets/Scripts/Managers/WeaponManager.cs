using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    public List<Weapon> pistols = new();
    public List<Weapon> rifles = new();
    public List<Weapon> shotguns = new();
    public List<Weapon> snipers = new();
    public Dictionary<string, List<Weapon>> weaponCategories = new Dictionary<string, List<Weapon>>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
        // Tilføj våbnene til de forskellige kategorier
        AddWeaponsToCategory(WeaponNames.PISTOL, pistols);
        AddWeaponsToCategory(WeaponNames.RIFLE, rifles);
        AddWeaponsToCategory(WeaponNames.SHOTGUN, shotguns);
        AddWeaponsToCategory(WeaponNames.SNIPER, snipers);
    }

    private void Start()
    {
        UpdatePlayerWeaponsWithHighestLevelsBought();
        Debug.Log(GetNextUnlockableWeaponByKey(WeaponNames.PISTOL));
        Debug.Log(GetHighestBoughtWeaponByKey(WeaponNames.PISTOL));
    }

    private void AddWeaponsToCategory(string category, List<Weapon> weapons)
    {
        if (!weaponCategories.ContainsKey(category))
        {
            weaponCategories[category] = new List<Weapon>();
        }

        //Add weapons to the given category
        weaponCategories[category].AddRange(weapons);

        //Sort by level
        weaponCategories[category] = weaponCategories[category].OrderBy(w => w.GetRangedWeaponDataSO().level).ToList();
    }

    /*
    public List<Weapon> GetWeaponsByKey(string key)
    {
        List<Weapon> tempList = new List<Weapon>();
        foreach (Weapon weapon in weaponCategories[key])
        {
            tempList.Add(weapon);
        }
        return tempList;
    }
    */

    public Weapon GetHighestBoughtWeaponByKey(string key)
    {
        List<Weapon> weapons = weaponCategories[key];

        Weapon highestLevelBoughtWeapon = weapons.Where(w => w.GetComponent<Weapon>().GetRangedWeaponDataSO().isBought)
        .OrderByDescending(w => w.GetComponent<Weapon>().GetRangedWeaponDataSO().level)
        .FirstOrDefault()?.GetComponent<Weapon>();

        return highestLevelBoughtWeapon;
    }

    public Weapon GetNextUnlockableWeaponByKey(string key)
    {
        foreach (Weapon weapon in weaponCategories[key])
        {
            if (!weapon.GetRangedWeaponDataSO().isBought)
            {
                return weapon;
            }
        }
        return GetHighestBoughtWeaponByKey(key);
    }

    public void BuyNextWeaponByKey(string key)
    {
        Weapon weapon = GetNextUnlockableWeaponByKey(key);
        RangedWeaponDataSO weaponData = weapon.GetRangedWeaponDataSO();
        if (weaponData.price <= Coins.Instance.GetCoins())
        {
            Coins.Instance.SpendCoins(weaponData.price);
            weapon.GetRangedWeaponDataSO().isBought = true;
            ShopUI.Instance.UpdateWeaponDataSO();
        }
        else
        {
            Debug.Log("VÅBNET ER FOR DYRT!!");
        }
    }

    public void UpdatePlayerWeaponsWithHighestLevelsBought()
    {
        WeaponHandling weaponHandling = WeaponHandling.Instance;
        Transform parent = weaponHandling.GetWeaponParent();
        List<Weapon> tempList = new()
        {
            GetHighestBoughtWeaponByKey(WeaponNames.PISTOL),
            GetHighestBoughtWeaponByKey(WeaponNames.RIFLE),
            GetHighestBoughtWeaponByKey(WeaponNames.SHOTGUN),
            GetHighestBoughtWeaponByKey(WeaponNames.SNIPER)
        };

        weaponHandling.ClearWeaponsList();

        foreach (Weapon weapon in tempList)
        {
            if (weapon != null)
            {
                Weapon tempWeapon = Instantiate(weapon, parent.position + weaponHandling.GetWeaponOffset(), parent.rotation, parent);
                weaponHandling.AddWeaponToWeaponsList(tempWeapon);
                tempWeapon.gameObject.SetActive(false);
            }
        }
        weaponHandling.ChangeWeapon(0);
    }
}
