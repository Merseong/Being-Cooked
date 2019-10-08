using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTextObject : MonoBehaviour
{
    public FoodData foodData;

    public GameObject commentObj;
    public TextMesh titleText;
    public TextMesh[] commentText = new TextMesh[4];

    private void Start()
    {
        titleText.text = foodData.boardName;
        RefreshComment();
    }

    public void RefreshComment()
    {
        for (int i = 0; i < 4; i++)
        {
            if (foodData.isCommented[i])
            {
                try
                {
                    if (foodData.comments[i].Contains("\n"))
                    {
                        commentText[i].text = foodData.comments[i].Trim();
                    }
                    else
                    {
                        commentText[i].text = "...";
                    }
                }
                catch (Exception e)
                {
                    commentText[i].text = "...";
                }
            }
            else
            {
                commentText[i].text = "???";
            }
        }
    }

    private void OnMouseEnter()
    {
        commentObj.SetActive(true);
    }

    private void OnMouseExit()
    {
        commentObj.SetActive(false);
    }
}
