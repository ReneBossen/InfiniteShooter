using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged Weapon")]
public class RangedWeaponDataSO : WeaponSO
{
    [Header("Ranged Weapon Settings")]
    public int maxAmmo;
    public float reloadTime;
    public float fireRate;
    public float bulletSpread;
    public int bulletUsePerShot;
    //Currently out of the game (KNOCKBACK)
    //public float knockBackForce;
    public int pierceAmount;
    public bool isAuto;
    public ParticleSystem shootingParticle;
    public TrailRenderer bulletTrail;
    [HideInInspector] public Transform bulletSpawnPoint;
}
