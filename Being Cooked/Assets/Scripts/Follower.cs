using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    Rigidbody rb;
    public CharacterMove cMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (cMove.enabled & cMove.followPos.Count >= cMove.followCount)
        {
            var dir = (cMove.followPos[Random.Range(1, 20)] - transform.position).normalized;
            var dist = Mathf.Lerp(0, 1, Vector3.Distance(transform.position, cMove.transform.position) / 5);
            rb.AddForce(dir * dist * cMove.speed, ForceMode.Acceleration);
        }
    }
}
