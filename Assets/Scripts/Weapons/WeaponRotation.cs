using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public static WeaponRotation Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is not null");
        }
        else Instance = this;
    }
    private void Update()
    {
        Aim();
    }
    public void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 aimDir = mousePos - transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        transform.localScale = aimLocalScale;
    }
}
