using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            var ing = other.GetComponent<Ingredient>();
            GameManager.inst.cameraFollow.StopAndResetCamera(2, GameManager.inst.pot.transform);
            ing.canControl = false;
            StartCoroutine(WaitForExitControl(ing));
        }
    }

    IEnumerator WaitForExitControl(Ingredient ing)
    {
        yield return new WaitForSeconds(1);
        ing.ExitControl();
    }
}
