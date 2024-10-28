using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ScriptableObject
{
    [Header("Base Weapon Settings")]
    public string weaponName;
    public Sprite sprite;
    public int damage;
    public float range;
    public int level;
    public int price;
    public bool isBought;
    public ParticleSystem impactParticle;
    public LayerMask layerMask;
}
