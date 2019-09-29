using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivity = 10;
    public float underLimit = 0.1f;

    public Transform parent;
    public Transform target;

    public bool isMoving = true;

    private void Start()
    {
        if (isMoving)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    isMoving = false;
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}

        float mouseMoveX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseMoveY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.RotateAround(target.position, Vector3.up, mouseMoveX);
        if (transform.position.y > underLimit || mouseMoveY < 0) transform.RotateAround(target.position, transform.right, -mouseMoveY);
        if (transform.position.y <= underLimit) transform.RotateAround(target.position, transform.right, mouseMoveY);
    }

    private void LateUpdate()
    {
        Vector3 camToParent = parent.position - transform.position;
        camToParent.y = 0;

        parent.forward = camToParent;
    }
}
