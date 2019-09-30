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

    public void ResetCamera()
    {
        GameManager.inst.cameraPoser.GetComponentInChildren<CameraMove>().MoveTo(GameManager.inst.transform, 0, false);
        GameManager.inst.cameraPoser.GetComponent<CameraFollow>().target = GameManager.inst.transform;
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
}
