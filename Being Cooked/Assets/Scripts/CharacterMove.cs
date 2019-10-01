using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 30;
    public List<Vector3> followPos = new List<Vector3>();
    [HideInInspector]
    public int followCount = 20;

    private bool isJumped = false;
    public bool isControlled = false;

    Rigidbody rb;
    Ingredient ingredient;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Respawn")) isJumped = false;
    }

    private void OnEnable()
    {
        isControlled = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ingredient = GetComponent<Ingredient>();
    }

    private void Update()
    {
        if (isControlled)
        {
            Vector3 horizontal = GameManager.inst.director.right * Input.GetAxis("Horizontal") * speed;
            Vector3 vertical = GameManager.inst.director.forward * Input.GetAxis("Vertical") * speed;

            rb.AddForce(horizontal + vertical);
        }

        // jump control
        if (!isJumped && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * speed / 2, ForceMode.VelocityChange);
            isJumped = true;
        }

        // follower position reference
        if (followPos.Count < followCount) followPos.Add(transform.position);
        else
        {
            followPos.RemoveAt(0);
            followPos.Add(transform.position);
        }
    }
}