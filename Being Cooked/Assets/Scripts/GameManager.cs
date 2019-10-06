using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private void Awake()
    {
        if (inst != null) Destroy(inst);
        inst = this;
    }

    public Transform cameraPoser;
    public CameraFollow cameraFollow;
    public CameraMove cameraMove;
    public Transform director;
    public CookingPot pot;

    public float[] finalTaste;
    public HashSet<string> finalIngredients = new HashSet<string>();

    public float[] recipeTaste = new float[]
    {
        0, 70, 90, 10, 30, 50
    };
    public string[] recipeIngredients = new string[]
    {
        "MaraSauce", "Mokee", "Chili"
    };
}
