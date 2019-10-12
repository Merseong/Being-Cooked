using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool isStoped = false;

    public float smoothSpeed = 0.25f;

    private void FixedUpdate()
    {
        if (!isStoped)
        {
            Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);
            transform.position = smoothedPos;
        }
    }

    public void StopAndResetCamera(float time)
    {
        StartCoroutine(StopCameraForSecond(time));
    }

    public void StopCameraForChanger(float time, Ingredient ing)
    {
        ing.canControl = false;
        StartCoroutine(JustStopCameraForSecond(time, ing));
    }

    public void ResetCamera()
    {
        UIManager.inst.midDot.enabled = true;
        GameManager.inst.cameraMove.MoveTo(GameManager.inst.transform, 0, GameManager.inst.pot.transform, true);
        GameManager.inst.cameraMove.isTargeting = false;
        isStoped = false;
    }

    IEnumerator StopCameraForSecond(float time)
    {
        float timer = 0;
        while (timer < time)
        {
            yield return new WaitForFixedUpdate();
            isStoped = true;
            timer += Time.deltaTime;
        }
        ResetCamera();
    }

    IEnumerator JustStopCameraForSecond(float time, Ingredient ing)
    {
        Debug.Log("Camera Stop!");
        float timer = 0;
        while (timer < time)
        {
            yield return new WaitForFixedUpdate();
            isStoped = true;
            timer += Time.deltaTime;
        }
        isStoped = false;
        GameManager.inst.Typechanger0.elapseIngredient(ing);

    }
}
