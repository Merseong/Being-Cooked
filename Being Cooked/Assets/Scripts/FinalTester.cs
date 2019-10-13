using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalTester : MonoBehaviour
{
    public GameObject mainPlane;
    public Text answerText;
    public SpriteRenderer spriteRenderer;
    public Text timerText;

    public Sprite doneSprite;

    private CookingPot pot;

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        pot = GameManager.inst.pot;
        pot.transform.position = mainPlane.transform.position;
        timerText.text = GameManager.inst.sw.Elapsed.ToString();

        CheckRecipe();
    }

    private int[] tasteArea = new int[]
    {
        0, 25, 50, 75, 100
    };
    private bool[] isInArea = new bool[6];

    void CheckRecipe()
    {
        bool isPerpect = true;
        // check taste
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Debug.Log(i + " and " + j);
                if (GameManager.inst.recipeTaste[i] > tasteArea[j] && GameManager.inst.recipeTaste[i] <= tasteArea[j + 1] &&
                    GameManager.inst.finalTaste[i] > tasteArea[j] && GameManager.inst.recipeTaste[i] <= tasteArea[j + 1])
                {
                    isInArea[i] = true;
                    isPerpect = false;
                    break;
                }   
            }
            if (!isInArea[i])
            {
                switch(i)
                {
                    case 0: // 단맛
                        answerText.text += "단맛이 ";
                        break;
                    case 1: // 짠맛
                        answerText.text += "짠맛이 ";
                        break;
                    case 2: // 매운맛
                        answerText.text += "매운맛이 ";
                        break;
                    case 3: // 신맛
                        answerText.text += "신맛이 ";
                        break;
                    case 4: // 쓴맛
                        answerText.text += "쓴맛이 ";
                        break;
                    case 5: // 감칠맛
                        answerText.text += "감칠맛이 ";
                        break;
                }
                if (GameManager.inst.recipeTaste[i] - GameManager.inst.finalTaste[i] < 0)
                {
                    answerText.text += "과한 것 같다.\n";
                }
                else
                {
                    answerText.text += "부족한 것 같다.\n";
                }
            }
        }
        answerText.text += "\n";
        for (int i = 0; i < GameManager.inst.recipeIngredients.Length; i++)
        {
            //Debug.Log(GameManager.inst.recipeIngredients[i] + " and " + GameManager.inst.finalIngredients.Contains(GameManager.inst.recipeIngredients[i]));
            if (!GameManager.inst.finalIngredients.Contains(GameManager.inst.recipeIngredients[i]))
            {
                answerText.text += "중요한 재료가 몇 부족한것같다.\n";
                isPerpect = false;
                break;
            }
        }
        if (isPerpect)
        {
            answerText.text += "완벽한 요리다!!\n";
            spriteRenderer.sprite = doneSprite;
        }
    }
}
