using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSOComparer : IComparer<WeaponSO>
{
    public int Compare(WeaponSO x, WeaponSO y)
    {
        if (x == null)
        {
            if (y == null)
                return 0;
            else
                return 1;
        }
        else
        {
            if (y == null)
                return 1;
            else
                return x.level.CompareTo(y.level);
        }
    }
}
