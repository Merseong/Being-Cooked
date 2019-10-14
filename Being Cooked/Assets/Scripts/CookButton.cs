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

    public GameObject scriptBox;
    public Text script;
    public string[,] textTaste = new string[6, 4]
        {
            { "단맛이 안난다. ","좀 더 달아야 할 것 같다. ","달콤하다. ","너무 달다. " },
            { "너무 싱겁다. ","삼삼하다. ","짭짤하다. ","너무 짜다. " },
            { "너무 안맵다. ","좀 더 매웠으면 좋겠다. ","매콤하다. ","너무 맵다. " },
            { "시큼하지 않다. ","좀 더 시큼해도 좋을 것 같다. ","시큼하다. ","너무 시다. " },
            { "쓴 맛이 존재하지 않는 것 같다. ","좀 더 씁쓸하면 좋을 것 같다. ","씁쓸하다. ","약맛이 난다. " },
            { "msg를 좀 넣어야 할 것 같다. ","감칠맛이 부족한 것 같다. ","감칠맛이 난다. ","감칠맛이 과한것 같다. " },
        }; // 맛에 따른 맛보기 문장 집어넣어야함

    public void cookTaste()
    {
        finalFlavor = GameManager.inst.pot.decideFlavor();
        scriptBox.SetActive(true);
        StartCoroutine(tasteScript());
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
        GameManager.inst.cameraFollow.target = GameManager.inst.transform;
        GameManager.inst.sw.Stop();
        GameManager.inst.isTimeTracking = false;
        GameManager.inst.pot.isCooking = false;
        GameManager.inst.pot.transform.parent = null;
        SceneManager.MoveGameObjectToScene(GameManager.inst.pot.gameObject, finalScene);
        SceneManager.MoveGameObjectToScene(GameManager.inst.gameObject, finalScene);

        Camera.main.gameObject.SetActive(false);
        UIManager.inst.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Cook Taste");
            cookTaste();
            //위에 되는지 나중에 다시 확인, 눌리는 애니메이션? 추가 해야함
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 눌리는 애니메이션 추가해야됨
            Debug.Log("Cook End");
            cookEnd();
        }
    }

    IEnumerator tasteScript ()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                if (finalFlavor[i] < 25)
                {
                    script.text = textTaste[i, 0];
                }
                else if (finalFlavor[i] < 50)
                {
                    script.text = textTaste[i, 1];
                }
                else if (finalFlavor[i] < 75)
                {
                    script.text = textTaste[i, 2];
                }
                else if (finalFlavor[i] <= 100)
                {
                    script.text = textTaste[i, 3];
                }
                else
                {
                    script.text = "dirty hacker!";
                }
            }
            else if(i%2 == 1)
            {
                if (finalFlavor[i] < 25)
                {
                    script.text += "\t" + textTaste[i, 0];
                }
                else if (finalFlavor[i] < 50)
                {
                    script.text += "\t" + textTaste[i, 1];
                }
                else if (finalFlavor[i] < 75)
                {
                    script.text += "\t" + textTaste[i, 2];
                }
                else if (finalFlavor[i] <= 100)
                {
                    script.text += "\t" + textTaste[i, 3];
                }
                else
                {
                    script.text = "dirty hacker!";
                }
            }
            else
            {
                if (finalFlavor[i] < 25)
                {
                    script.text += "\n" + textTaste[i, 0];
                }
                else if (finalFlavor[i] < 50)
                {
                    script.text += "\n" + textTaste[i, 1];
                }
                else if (finalFlavor[i] < 75)
                {
                    script.text += "\n" + textTaste[i, 2];
                }
                else if (finalFlavor[i] <= 100)
                {
                    script.text += "\n" + textTaste[i, 3];
                }
                else
                {
                    script.text = "dirty hacker!";
                }
            }
            Debug.Log("Yammy!, " + finalFlavor[i]);
            yield return new WaitForSeconds(1);
        }
        scriptBox.SetActive(false);
    }
}
