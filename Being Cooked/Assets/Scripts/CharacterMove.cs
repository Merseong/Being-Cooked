using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 30;

    public Transform parent;
    public Transform director;

    private bool isJumped = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Respawn")) isJumped = false;
    }

    private void Update()
    {
        Vector3 horizontal = director.right * Input.GetAxis("Horizontal") * speed;
        Vector3 vertical = director.forward * Input.GetAxis("Vertical") * speed;

        GetComponent<Rigidbody>().AddForce(horizontal + vertical);

        if (!isJumped && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed / 2, ForceMode.VelocityChange);
            isJumped = true;
        }
    }

    private void LateUpdate()
    {
        parent.position = transform.position;
    }
}