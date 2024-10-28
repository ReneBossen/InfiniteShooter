using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 playerPos = Player.Instance.transform.position;
        playerPos.z = 0;

        lineRenderer.SetPosition(0, playerPos);
        lineRenderer.SetPosition(1, mousePos);
    }
}
