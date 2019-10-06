using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    public Button tasteButton;
    public Button endButton;
    public float[] finalFlavor = new float[6];
    [HideInInspector]
    public float[] goalFlavor = new float[6];
    public CookingPot pot;
    public Image scriptBox;
    public Text script;
    public string[] textTaste = new string[48]; // 맛에 따른 맛보기 문장 집어넣어야함

    public void cookTaste()
    {
        finalFlavor = pot.decideFlavor();
        scriptBox.gameObject.SetActive(true);
        for(int i =0; i <6; i++)
        {
            if (finalFlavor[i] < -75)
            {
                script.text = textTaste[i * 8 - 8];
            }
            else if(finalFlavor[i] < -50)
            {
                script.text = textTaste[i * 8 - 7];
            }
            else if(finalFlavor[i] < -25)
            {
                script.text = textTaste[i * 8 - 6];
            }
            else if(finalFlavor[i] < 0)
            {
                script.text = textTaste[i * 8 - 5];
            }
            else if(finalFlavor[i] < 25)
            {
                script.text = textTaste[i * 8 - 4];
            }
            else if(finalFlavor[i] < 50)
            {
                script.text = textTaste[i * 8 - 3];
            }
            else if(finalFlavor[i] < 75)
            {
                script.text = textTaste[i * 8 - 2];
            }
            else
            {
                script.text = textTaste[i * 8 - 1];
            }
            while (!Input.GetMouseButtonDown(0))//얘도 키다운으로 바꿀까
            {

            }
            Debug.Log("Yammy!");
        }
        scriptBox.gameObject.SetActive(false);
    }
    public void cookEnd()
    {
        finalFlavor = pot.decideFlavor();
        goalFlavor = pot.goal.goalFlavor;
        //아마 엔딩 씬으로 변환
        Debug.Log("The end");
    }
    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //cookTaste();
            //위에 되는지 나중에 다시 확인, 눌리는 애니메이션? 추가 해야함
            Debug.Log("Cook Taste");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //cookEnd(); 
            //위에 되는지 나중에 다시 확인, 눌리는 애니메이션? 추가 해야함
            Debug.Log("Cook End");
        }

    }
}
