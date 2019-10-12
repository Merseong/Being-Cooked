using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    private void Awake()
    {
        if (inst != null) Destroy(inst);
        inst = this;
    }

    public Stopwatch sw = new Stopwatch();
    public Text swText;
    public bool isTimeTracking = true;

    private void Start()
    {
        sw.Reset();
        sw.Start();
    }

    private void Update()
    {
        if (isTimeTracking)
        {
            swText.text = sw.Elapsed.ToString();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    public Transform cameraPoser;
    public CameraFollow cameraFollow;
    public CameraMove cameraMove;
    public Transform director;
    public CookingPot pot;

    public float[] finalTaste;
    public HashSet<string> finalIngredients = new HashSet<string>();

    public TypeChanger0 Typechanger0;
    public TypeChanger1 Typechanger1;

    public float[] recipeTaste = new float[]
    {
        0, 70, 90, 10, 30, 50
    };
    public string[] recipeIngredients = new string[]
    {
        "MaraSauce", "Mokee", "Chili"
    };
}
