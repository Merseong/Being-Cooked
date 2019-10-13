using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollContentMouse : MonoBehaviour
{
    public ScrollRect scroll;
    [SerializeField] float ScrollSpeed = 100;

    private void Update()
    {
        if (Input.GetAxis("ScrollWheel") != 0)
        {
            scroll.content.anchoredPosition += new Vector2(0, -Input.GetAxis("ScrollWheel") * ScrollSpeed);
        }
    }
}
