using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Solid : Ingredient
{
    public override void IntoPot()
    {
        GameManager.inst.cameraFollow.StopAndResetCamera(2, GameManager.inst.pot.transform);
        characterMove.isControlled = false;
        Invoke("StopMove", 1);
        canControl = false;
    }

    void StopMove()
    {
        characterMove.enabled = false;
        UIManager.inst.GenerateFoodBar(this as Ingredient);
        AfterPot();
    }
}
