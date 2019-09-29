using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.25f;

    private void FixedUpdate()
    {
        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = smoothedPos;
    }
}
