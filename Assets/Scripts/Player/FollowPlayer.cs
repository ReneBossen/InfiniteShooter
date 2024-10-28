using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float smoothSpeed;
    private Transform target;
    private void Start()
    {
        target = Player.Instance.transform;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPostiion = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPostiion, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
