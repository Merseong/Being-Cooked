using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    public Button tasteButton;
    public Button endButton;
    public float[] finalFlavor = new float[6];
    public float[] goalFlavor = new float[6];
    public CookingPot pot;
    public Text script;
    public string[] textTaste = new string[24]; // 맛에 따른 맛보기 문장 집어넣어야함

    public void cookTaste()
    {
        finalFlavor = pot.decideFlavor();
        for(int i =0; i <6; i++)
        {
            if (finalFlavor[i] < -50)
            {
                script.text = textTaste[i];
            }
            else if(finalFlavor[i] < 0)
            {
                script.text = textTaste[i*2];
            }
            else if(finalFlavor[i] < 50)
            {
                script.text = textTaste[i*3];
            }
            else
            {
                script.text = textTaste[i*4];
            }

        }
    }
    public void cookEnd()
    {
        finalFlavor = pot.decideFlavor();
        goalFlavor = pot.goal.goalFlavor;
        //아마 엔딩 씬으로 변환
    }
    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Update()
    {
        /*if (pot으로 들어갔을때)
        {
            gameObject.SetActive(true);
        }*/

    }
}
