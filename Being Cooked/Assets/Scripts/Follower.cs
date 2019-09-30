using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (cMove.enabled & cMove.followPos.Count >= cMove.followCount) rb.AddForce((cMove.followPos[Random.Range(1,20)] - transform.position) * cMove.speed / 4);
    }
}
