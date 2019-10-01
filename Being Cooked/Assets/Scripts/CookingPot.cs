using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CookingPot : MonoBehaviour
{
    public Transform camPos;

    private void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            other.GetComponent<Ingredient>().IntoPot();
        }
    }
}
