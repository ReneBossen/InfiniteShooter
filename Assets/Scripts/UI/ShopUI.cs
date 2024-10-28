using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] Image pistolSprite;
    [SerializeField] TextMeshProUGUI pistolText;
    [SerializeField] Image rifleSprite;
    [SerializeField] TextMeshProUGUI rifleText;
    [SerializeField] Image shotgunSprite;
    [SerializeField] TextMeshProUGUI shotgunText;
    [SerializeField] Image sniperSprite;
    [SerializeField] TextMeshProUGUI sniperText;

    private RangedWeaponDataSO weaponDataPistol;
    private RangedWeaponDataSO weaponDataRifle;
    private RangedWeaponDataSO weaponDataShotgun;
    private RangedWeaponDataSO weaponDataSniper;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
    }

    private void Start()
    {
        UpdateWeaponDataSO();
        //Set sprite to be the next unlockable level
        UpdateSprites();
        Hide();
    }
    public void UpdateWeaponDataSO()
    {
        weaponDataPistol = WeaponManager.Instance.GetNextUnlockableWeaponByKey(WeaponNames.PISTOL).GetRangedWeaponDataSO();
        weaponDataRifle = WeaponManager.Instance.GetNextUnlockableWeaponByKey(WeaponNames.RIFLE).GetRangedWeaponDataSO();
        weaponDataShotgun = WeaponManager.Instance.GetNextUnlockableWeaponByKey(WeaponNames.SHOTGUN).GetRangedWeaponDataSO();
        weaponDataSniper = WeaponManager.Instance.GetNextUnlockableWeaponByKey(WeaponNames.SNIPER).GetRangedWeaponDataSO();
        UpdateSprites();
        UpdatePriceButton();
        UpdateCoins();
    }

    private void UpdateSprites()
    {
        pistolSprite.sprite = weaponDataPistol.sprite;
        rifleSprite.sprite = weaponDataRifle.sprite;
        shotgunSprite.sprite = weaponDataShotgun.sprite;
        sniperSprite.sprite = weaponDataSniper.sprite;
    }

    private void UpdatePriceButton()
    {
        pistolText.text = "BUY PISTOL\n$" + weaponDataPistol.price;
        rifleText.text = "BUY RIFLE\n$" + weaponDataRifle.price;
        shotgunText.text = "BUY SHOTGUN\n$" + weaponDataShotgun.price;
        sniperText.text = "BUY SNIPER\n$" + weaponDataSniper.price;
    }

    public void CloseShopToGameOver()
    {
        GameOverUI.Instance.Show();
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdatePriceButton();
        UpdateCoins();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void BuyWeapon(string key)
    {
        WeaponManager.Instance.BuyNextWeaponByKey(key);
    }

    private void UpdateCoins()
    {
        coinsText.text = Coins.Instance.GetCoins().ToString();
    }
}