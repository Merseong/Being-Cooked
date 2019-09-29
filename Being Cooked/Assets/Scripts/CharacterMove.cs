using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 30;

    private bool isJumped = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Respawn")) isJumped = false;
    }

    private void Update()
    {
        Vector3 horizontal = GameManager.inst.director.right * Input.GetAxis("Horizontal") * speed;
        Vector3 vertical = GameManager.inst.director.forward * Input.GetAxis("Vertical") * speed;

        GetComponent<Rigidbody>().AddForce(horizontal + vertical);

        if (!isJumped && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * speed / 2, ForceMode.VelocityChange);
            isJumped = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Ingredient>().ExitControl();
        }
    }
}