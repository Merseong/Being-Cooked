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

    private void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
        mat = GetComponentInChildren<MeshRenderer>().material;
        soupColor = mat.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            addIngred.Add(other.gameObject);
            other.GetComponent<Ingredient>().IntoPot();
            Debug.Log(other.GetComponent<Ingredient>().soupColor);
            StartCoroutine(ChangingColor(other.GetComponent<Ingredient>().soupColor));
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
