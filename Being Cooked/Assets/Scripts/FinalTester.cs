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

    [Header("Score")]
    public int score = 500;
    public Text yourScore;
    public Text TopScore;
    public Text TopScoreTime;

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        pot = GameManager.inst.pot;
        pot.transform.position = mainPlane.transform.position;
        timerText.text = GameManager.inst.sw.Elapsed.ToString();

        TopScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        TopScoreTime.text = PlayerPrefs.GetString("HighScoreTime");

        CheckRecipe();
    }

    private bool[] isInArea = new bool[6];

    void CheckRecipe()
    {
        bool isPerpect = true;
        // check taste
        for (int i = 0; i < 6; i++)
        {
            score += 100 - Mathf.Abs(Mathf.RoundToInt(GameManager.inst.recipeTaste[i] - GameManager.inst.finalTaste[i]));
            if (GameManager.inst.recipeTaste[i] / 25 == GameManager.inst.finalTaste[i] / 25)
            {
                isInArea[i] = true;
                isPerpect = false;
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
                score -= 500;
                isPerpect = false;
                break;
            }
        }
        if (isPerpect)
        {
            answerText.text += "완벽한 요리다!!\n";
            score += 500;
            spriteRenderer.sprite = doneSprite;
        }
        yourScore.text = score.ToString();
    }

    public void BackToTitle()
    {
        int timeElapseInt = System.Convert.ToInt32(GameManager.inst.sw.ElapsedMilliseconds / 1000);
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetString("HighScoreTime", timerText.text.Substring(0, 8));
            PlayerPrefs.SetInt("HighScoreTimeInt", timeElapseInt);
        }
        else if (PlayerPrefs.GetInt("HighScore") == score && PlayerPrefs.GetInt("HighScoreTimeInt") > timeElapseInt)
        {
            PlayerPrefs.SetString("HighScoreTime", timerText.text.Substring(0, 8));
            PlayerPrefs.SetInt("HighScoreTimeInt", timeElapseInt);
        }
        SceneManager.LoadScene("StartScene");
    }
}
