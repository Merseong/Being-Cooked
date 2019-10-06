using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class CookingPot : MonoBehaviour
{
    public Transform camPos;
    public Transform spicePos;
    private List<GameObject> addIngred = new List<GameObject>();

    public float[] finalFlavor = new float[6];
    public Color soupColor;

    [HideInInspector]
    public float cTime;

    private Material mat;
    private float lastCol = 0;

    private void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
        mat = GetComponentInChildren<MeshRenderer>().material;
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
                ing.IntoPot();
                //Debug.Log(ing.soupColor);
                StartCoroutine(ChangingColor(ing.soupColor));
            }
            else if (ing.type == IngredientType.Spice)
            {
                var spice = other.GetComponent<Spice>();
                spice.IntoPot();
                addIngred.Add(spice.GetSpiceObj());
                StartCoroutine(ChangingColor(spice.soupColor));
            }
        }
    }
    IEnumerator ChangingColor(Color color)
    {

        float totalTime = 4;
        cTime = 0;
        for (; cTime < totalTime; cTime += Time.deltaTime)
        {
            soupColor = Color.Lerp(soupColor, soupColor * color, cTime / totalTime);
            mat.color = soupColor;
            yield return null;
        }
    }
    public float[] decideFlavor() //생각 생각
    {
        for (int a = 0; a < 6; a++)
        {
            finalFlavor[a] = 0;
        }
        for (int i = 0; i < addIngred.Count; i++)
        {
            float[] flavor = addIngred[i].GetComponent<Ingredient>().GetFlavor();

            for (int j = 0; j < 6; j++)
            {
                finalFlavor[j] += flavor[j];
            }
        }
        return finalFlavor;
    }

    private void Update()
    {
        for (int i = 0; i < addIngred.Count; i++)
        {
            addIngred[i].GetComponent<Ingredient>().cookedTime += Time.deltaTime;
        }
        
    }
}
