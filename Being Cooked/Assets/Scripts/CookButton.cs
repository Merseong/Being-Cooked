using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CookButton : MonoBehaviour
{
    public Button tasteButton;
    public Button endButton;
    public float[] finalFlavor = new float[6];

    public Image scriptBox;
    public Text script;
    public string[] textTaste = new string[48]; // 맛에 따른 맛보기 문장 집어넣어야함

    public void cookTaste()
    {
        finalFlavor = GameManager.inst.pot.decideFlavor();
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
        GameManager.inst.cameraMove.isCameraMoving = true;

        finalFlavor = GameManager.inst.pot.decideFlavor();
        GameManager.inst.finalTaste = finalFlavor;
        for (int i = 0; i < GameManager.inst.pot.addIngred.Count; i++)
        {
            GameManager.inst.finalIngredients.Add(GameManager.inst.pot.addIngred[i].GetComponent<Ingredient>().foodName);
            //Debug.Log(GameManager.inst.pot.addIngred[i].GetComponent<Ingredient>().foodName);
        }

        SceneManager.LoadScene("FinalScene", LoadSceneMode.Additive);
        var finalScene = SceneManager.GetSceneByName("FinalScene");
        GameManager.inst.pot.isCooking = false;
        GameManager.inst.pot.transform.parent = null;
        SceneManager.MoveGameObjectToScene(GameManager.inst.pot.gameObject, finalScene);
        SceneManager.MoveGameObjectToScene(GameManager.inst.gameObject, finalScene);

        Camera.main.gameObject.SetActive(false);
        UIManager.inst.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
            // 눌리는 애니메이션 추가해야됨
            Debug.Log("Cook End");
            cookEnd();
        }
    }
}
