﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Solid : Ingredient
{
    public override void AfterPot()
    {
        throw new System.NotImplementedException();
    }

    public override void IntoPot()
    {
        GameManager.inst.cameraFollow.StopAndResetCamera(2);
        characterMove.isControlled = false;
        Invoke("StopMove", 1);
        canControl = false;
    }

    void StopMove()
    {
        characterMove.enabled = false;
    }
}
