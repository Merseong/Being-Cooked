using System.Collections;
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

[RequireComponent(typeof (CharacterMove))]
[RequireComponent(typeof (Outline))]
[SerializeField]
public abstract class Ingredient : MonoBehaviour
{
    [Header("Ingredient Value")]
    public string foodName = "";
    public float cookingTime = 10;
    public bool isProcessed = false;
    public Color soupColor;
    public float size = 3;
    [HideInInspector]
    public float cookedTime = 0;
    [HideInInspector]
    public bool canControl = true;

    [Header("Flavor Value")]
    [Range(-20, 20)]
    public float[] beforeFlavor = new float[6];
    [Range(-20, 20)]
    public float[] afterFlavor = new float[6];

    protected CharacterMove characterMove;

    public abstract void IntoPot();

    public abstract void AfterPot();

    public void EnterControl()
    {
        GameManager.inst.cameraMove.MoveTo(transform, size);
        characterMove.enabled = true;
    }
    public void ExitControl()
    {
        GameManager.inst.cameraFollow.ResetCamera();
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

    private void Start()
    {
        GetComponent<Outline>().enabled = false;
        characterMove = GetComponent<CharacterMove>();
        characterMove.enabled = false;
    }
}
