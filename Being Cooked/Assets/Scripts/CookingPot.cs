using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CookingPot : MonoBehaviour
{
    public Transform camPos;
    public Transform spicePos;
    public List<GameObject> addIngred = new List<GameObject>();
    public List<Ingredient> addedIngre = new List<Ingredient>();
    public GameObject potObj;

    public float[] finalFlavor = new float[6];
    public Color soupColor;
    [HideInInspector]
    public Color save;

    public float changeTime = 4;
    public GoalFood goal;

    public bool isCooking = true;

    private Material mat;
    private float lastCol = 0;

    private void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
        mat = potObj.GetComponent<MeshRenderer>().material;
        soupColor = mat.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") && lastCol + 5 < Time.time)
        {
            lastCol = Time.time;
            var ing = other.GetComponent<Ingredient>();
            if (ing.type == IngredientType.Solid)
            {
                addIngred.Add(other.gameObject);
                addedIngre.Add(ing);
                ing.IntoPot();
                //Debug.Log(ing.soupColor);
                save = soupColor;
                StartCoroutine(ChangingColor(ing.soupColor));
            }
            else if (ing.type == IngredientType.Spice)
            {
                var spice = other.GetComponent<Spice>();
                spice.IntoPot();
                var spiceObj = spice.GetSpiceObj();
                addIngred.Add(spiceObj);
                addedIngre.Add(spiceObj.GetComponent<Ingredient>());
                save = soupColor;
                StartCoroutine(ChangingColor(spice.soupColor));
            }
        }
    }
    IEnumerator ChangingColor(Color color)
    {
        float cTime = 0;
        for (; cTime < changeTime; cTime += Time.deltaTime)
        {
            soupColor = Color.Lerp(save, save * color, cTime / changeTime);
            mat.color = soupColor;
            yield return null;
        }
    }
    public float[] decideFlavor() //잘 작동하나 확인해봐야함
    {
        for (int a = 0; a < 6; a++)
        {
            finalFlavor[a] = 0;
        }
        for (int i = 0; i < addIngred.Count; i++)
        {
            float[] flavor = addedIngre[i].GetFlavor();

            for (int j = 0; j < 6; j++)
            {
                finalFlavor[j] += flavor[j];
            }
        }
        for (int i = 0; i < 6; i++)
        {
            if (finalFlavor[i] > 100)
            {
                finalFlavor[i] = 100;
            }
            else if (finalFlavor[i] < 0)
            {
                finalFlavor[i] = 0;
            }
        }
        //Debug.Log(finalFlavor[0]);
        return finalFlavor;
    }

    public float maxBurned = 0;

    private void Update()
    {
        if (isCooking)
        {
            for (int i = 0; i < addIngred.Count; i++)
            {
                addedIngre[i].cookedTime += Time.deltaTime;
                if (maxBurned < addedIngre[i].cookedTime / addedIngre[i].cookingTime)
                {
                    maxBurned = addedIngre[i].cookedTime / addedIngre[i].cookingTime;
                }
            }
        }
    }
}
