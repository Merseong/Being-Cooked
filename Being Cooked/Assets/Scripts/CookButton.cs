using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    public Button tasteButton;
    public Button endButton;
    public float[] finalFlavor = new float[6];
    public CookingPot pot;

    public void cookTaste()
    {
        finalFlavor = pot.decideFlavor();
        for(int i =0; i <6; i++)
        {
            //if 
        }
    }
    public void cookEnd()
    {
        finalFlavor = pot.decideFlavor();

    }
    public void Start()
    {
        gameObject.SetActive(false);
    }
}
