﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public enum Flavor
{
    단맛,
    짠맛,
    매운맛,
    신맛,
    쓴맛,
    감칠맛
}

public enum IngredientType
{
    Solid,
    Spice
}

[RequireComponent(typeof (CharacterMove))]
[RequireComponent(typeof (Outline))]
[SerializeField]
public abstract class Ingredient : MonoBehaviour
{
    [Header("Ingredient Value")]
    public string foodName = "";
    public string boardName = "";
    public IngredientType type;
    public float cookingTime = 10;
    public bool isProcessed = false;
    public Color soupColor;
    public float size = 3;
    public float cookedTime = 0;
    public bool canControl = true;

    [Header("Flavor Value")]
    [Range(-20, 20)]
    public float[] beforeFlavor = new float[6];
    [Range(-20, 20)]
    public float[] afterFlavor = new float[6];

    [Header("Followers")]
    public GameObject followerObj;
    public int followerCount = 0;
    public List<Transform> followers;

    public CharacterMove characterMove;

    public abstract void IntoPot();

    public void AfterPot()
    {
        transform.parent = GameManager.inst.pot.transform;
        for (int i = 0; i < followers.Count; i++)
        {
            followers[i].parent = GameManager.inst.pot.transform;
        }
        BoardManager.inst.RemoveIngredient(this);
    }

    public void EnterControl()
    {
        GameManager.inst.cameraMove.MoveTo(transform, size);
        characterMove.enabled = true;
    }
    public void ExitControl()
    {
        GameManager.inst.cameraFollow.ResetCamera(transform);
        characterMove.enabled = false;
    }

    public float[] GetFlavor()
    {
        float[] flavor = new float[6];
        if (cookedTime <= cookingTime)
        {
            // before -> after
            for (int i = 0; i < 6; i++)
            {
                flavor[i] = Mathf.Lerp(beforeFlavor[i], afterFlavor[i], cookedTime / cookingTime);
            }
        }
        else if (cookedTime <= cookingTime * 1.5f)
        {
            // after -> wait
            for (int i = 0; i < 6; i++)
            {
                flavor[i] = afterFlavor[i];
            }
        }
        else if (cookedTime <= cookingTime * 3.5f)
        {
            // after -> -after
            for (int i = 0; i < 6; i++)
            {
                flavor[i] = Mathf.Lerp(afterFlavor[i], -afterFlavor[i], (cookedTime - cookingTime * 1.5f) / (cookingTime * 2));
            }
        }
        else
        {
            // fully burn
            for (int i = 0; i < 6; i++)
            {
                flavor[i] = -afterFlavor[i];
            }
        }
        return flavor;
    }
    private void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
        characterMove.enabled = false;
    }
    private void Start()
    {
        GetComponent<Outline>().enabled = false;
        BoardManager.inst.AddIngredient(this);
        AfterStart();
    }

    protected virtual void AfterStart()
    {

    }
}
