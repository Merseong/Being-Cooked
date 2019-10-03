using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    private void Awake()
    {
        if (inst != null) Destroy(inst);
        inst = this;
    }

    public Image midDot;

    public GameObject foodBar;
    private Vector2 nextBarPos = new Vector2();

    public void GenerateFoodBar(Ingredient ingre)
    {
        var bar = Instantiate(foodBar, transform).GetComponent<FoodGauge>();
        bar.Img.sprite = Resources.Load<Sprite>("FoodSprites/" + ingre.foodName + (ingre.isProcessed ? 1 : 0)); 
        bar.assignedIng = ingre;
        bar.gameObject.GetComponent<RectTransform>().Translate(nextBarPos);
        nextBarPos += new Vector2(0, 85);
    }
}
