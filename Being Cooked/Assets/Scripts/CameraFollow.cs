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

    public void StopAndResetCamera(float time, Transform lookat)
    {
        StartCoroutine(StopCameraForSecond(time, lookat));
    }

    public void StopCameraForChanger(float time, Ingredient ing, bool a)
    {
        ing.canControl = false;
        if(a)
            StartCoroutine(JustStopCameraForSecond1(time, ing));
        else
            StartCoroutine(JustStopCameraForSecond0(time, ing));
    }

    public void ResetCamera(Transform lookat)
    {
        UIManager.inst.midDot.enabled = true;
        GameManager.inst.cameraMove.MoveTo(GameManager.inst.transform, 0, lookat, true);
        GameManager.inst.cameraMove.isTargeting = false;
        isStoped = false;
    }

    IEnumerator StopCameraForSecond(float time, Transform lookat)
    {
        float timer = 0;
        while (timer < time)
        {
            yield return new WaitForFixedUpdate();
            isStoped = true;
            timer += Time.deltaTime;
        }
        ResetCamera(lookat);
    }

    IEnumerator JustStopCameraForSecond0(float time, Ingredient ing)
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

    IEnumerator JustStopCameraForSecond1(float time, Ingredient ing)
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
        GameManager.inst.Typechanger1.elapseIngredient(ing);
    }
}
