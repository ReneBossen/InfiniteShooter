using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Weapon base attack");
        }
    }

    public virtual RangedWeaponDataSO GetRangedWeaponDataSO()
    {
        Debug.LogError("Not implementet");
        return null;
    }
}
