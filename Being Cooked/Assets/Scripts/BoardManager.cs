using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class FoodData
{
    public string foodName;
    public string boardName;
    public int count;

    public string[] comments;
    public bool[] isCommented;

    public GameObject textObj;
}

public class BoardManager : MonoBehaviour
{
    public static BoardManager inst;

    private void Awake()
    {
        if (inst != null) Destroy(inst);
        inst = this;
    }

    public GameObject textMeshObj;

    [SerializeField]
    private List<FoodData> dataList = new List<FoodData>();
    private Vector2 nextTextPos = new Vector2(-16, 6);

    private string[,] commentList = new string[,]
    {
        { "떫다 (달지않다)\n", "달달하다\n" }, // 단맛
        { "싱겁다\n", "짭짤하다\n" }, // 짠맛
        { "안맵다\n", "매콤하다\n" }, // 매운맛
        { "비리다 (시지않다)\n", "새콤하다\n" }, // 신맛
        { "쓰지않다\n", "꽤 쓰다\n" }, // 쓴맛
        { "감칠맛이 없다\n", "감칠맛이 돈다\n" }, // 감칠맛
    };

    public void AddIngredient(Ingredient ing)
    {
        if (dataList.Exists(x => x.foodName == ing.foodName))
        {
            FoodData data = dataList.Find(x => x.foodName == ing.foodName);
            data.count++;

            //Debug.Log(data.foodName + " " + data.count);
            for (int i = 0; i < 4; i++)
            {
                if (!data.isCommented[i])
                {
                    data.isCommented[i] = true;
                    data.isCommented[i + 1] = true;
                    for (int j = 0; j < 6; j++)
                    {
                        if (ing.beforeFlavor[j] <= -10)
                        {
                            data.comments[i] += commentList[j, 0];
                        }
                        else if (ing.beforeFlavor[j] >= 10)
                        {
                            data.comments[i] += commentList[j, 1];
                        }
                        if (ing.afterFlavor[j] <= -10)
                        {
                            data.comments[i + 1] += commentList[j, 0];
                        }
                        else if (ing.afterFlavor[j] >= 10)
                        {
                            data.comments[i + 1] += commentList[j, 1];
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            //Debug.Log("new " + ing.foodName);
            string[] newEx = new string[4];
            bool[] newCommented = new bool[4];
            // 0: original, non-cooked -> 1: original, cooked
            // 2: processed, non-cooked -> 3: processed, cooked
            int fillOffset = ing.isProcessed ? 2 : 0;
            for (int j = 0; j < 2; j++)
            {
                float[] toCheck = (j == 0 ? ing.beforeFlavor : ing.afterFlavor);
                for (int i = 0; i < 6; i++)
                {
                    if (toCheck[i] <= -10)
                    {
                        newEx[fillOffset + j] += commentList[i, 0];
                    }
                    else if (toCheck[i] >= 10)
                    {
                        newEx[fillOffset + j] += commentList[i, 1];
                    }
                }
                newCommented[fillOffset + j] = true;
            }

            FoodData newData = new FoodData
            {
                foodName = ing.foodName,
                boardName = ing.boardName,
                count = 1,
                comments = newEx,
                isCommented = newCommented
            };
            dataList.Add(newData);
            InstantiateText(newData);
        }
    }

    public void RemoveIngredient(Ingredient ing)
    {
        FoodData data = dataList.Find(x => x.foodName == ing.foodName);
        data.count--;

        //Debug.Log(data.foodName + " " + data.count);
        if (data.count == 0)
        {
            // Line on word
            RefreshText(data, true);
        }
    }

    void InstantiateText(FoodData data)
    {
        TextMesh textMesh = Instantiate(textMeshObj, transform).GetComponent<TextMesh>();
        textMesh.text = data.boardName;
        data.textObj = textMesh.gameObject;
        textMesh.GetComponent<BoardTextObject>().foodData = data;

        textMesh.transform.localPosition = nextTextPos;
        nextTextPos += new Vector2(0, -2.5f);
        if (nextTextPos.y < -10) nextTextPos = new Vector2(nextTextPos.x + 8, 6);
    }

    void RefreshText(FoodData data, bool isRunout = false)
    {
        if (isRunout)
        {
            string toAdd = "\n";
            for (int i = 0; i < data.boardName.Length * 2; i++) toAdd += "-";
            data.textObj.GetComponent<TextMesh>().text += toAdd;
        }
        else
        {
            data.textObj.GetComponent<BoardTextObject>().RefreshComment();
        }
    }
}
