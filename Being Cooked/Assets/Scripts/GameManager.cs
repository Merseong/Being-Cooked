﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private void Awake()
    {
        if (inst != null) Destroy(inst);
        inst = this;
    }

    public Transform cameraPoser;
    public CameraFollow cameraFollow;
    public CameraMove cameraMove;
    public Transform director;
    public CookingPot pot;
}
