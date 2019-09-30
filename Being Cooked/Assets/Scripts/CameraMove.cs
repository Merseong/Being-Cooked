using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivity = 10;
    public float underLimit = 0.1f;

    public bool isCameraMoving = false;
    public bool isTargeting = false;

    private Vector3 beforeLocalPos;
    private Quaternion beforeLocalRot;
    private Transform beforeHit;

    public void MoveTo(Transform target, float howFar, bool looking = true)
    {
        isCameraMoving = true;
        Debug.Log("enter control in " + target);
        StartCoroutine(Mover(target, howFar, looking));
    }

    IEnumerator Mover(Transform target, float howFar, bool looking)
    {
        float timer = 0;
        while (timer < 1f)
        {
            transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0,1,1) * howFar * 1.414f, timer);
            if (looking) transform.LookAt(target, Vector3.up);
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isCameraMoving = false;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    isMoving = false;
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        if (!isCameraMoving)
        {
            float mouseMoveX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseMoveY = Input.GetAxis("Mouse Y") * sensitivity;
            beforeLocalPos = transform.localPosition;
            beforeLocalRot = transform.localRotation;

            transform.RotateAround(GameManager.inst.cameraPoser.position, Vector3.up, mouseMoveX);
            transform.RotateAround(GameManager.inst.cameraPoser.position, transform.right, -mouseMoveY);
        }

        if (!isTargeting)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.transform.CompareTag("Ingredient"))
            {
                if (beforeHit != null && beforeHit != hit.transform)
                {
                    beforeHit.GetComponent<Outline>().enabled = false;
                    beforeHit = hit.transform;
                }
                else beforeHit = hit.transform;
                var ing = beforeHit.GetComponent<Ingredient>();
                if (ing.canControl)
                {
                    beforeHit.GetComponent<Outline>().enabled = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        ing.EnterControl();
                    }
                }
            }
            else if (beforeHit != null)
            {
                beforeHit.GetComponent<Outline>().enabled = false;
                beforeHit = null;
            }
        }
        else if (beforeHit != null)
        {
            beforeHit.GetComponent<Outline>().enabled = false;
            beforeHit = null;
        }
    }

    private void LateUpdate()
    {
        // Camera Clamping이긴 한데, Clamp지점에서 끊기게 움직여서 일단 보류
        //if (Vector3.Dot(Vector3.up, transform.up) < underLimit || transform.localPosition.y < underLimit)
        //{
        //    transform.localPosition = beforeLocalPos;
        //    transform.localRotation = beforeLocalRot;
        //}

        Vector3 camToParent = GameManager.inst.director.position - transform.position;
        camToParent.y = 0;

        if (camToParent.magnitude > 0) GameManager.inst.director.forward = camToParent;
    }
}
