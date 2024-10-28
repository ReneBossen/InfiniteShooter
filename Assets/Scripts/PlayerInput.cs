using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void WeaponChangeHandler(int weaponIndex);
    public static event WeaponChangeHandler OnInput1Pressed;
    public static event WeaponChangeHandler OnInput2Pressed;
    public static event WeaponChangeHandler OnInput3Pressed;
    public static event WeaponChangeHandler OnInput4Pressed;
    public static event EventHandler OnJumpPressed;

    private void Update()
    {
        {
            // Listen for input and call event
            if (Input.GetKeyDown(KeyCode.Alpha1))
                OnInput1Pressed?.Invoke(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                OnInput2Pressed?.Invoke(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                OnInput3Pressed?.Invoke(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                OnInput4Pressed?.Invoke(3);
            if (Input.GetKeyDown(KeyCode.Space))
                OnJumpPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
