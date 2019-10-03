using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodGauge : MonoBehaviour
{
    public Image Img;
    public Slider posSlider;
    public Image posSliderImg;
    public Slider negSlider;
    public Image negSliderImg;

    public Ingredient assignedIng;

    private void LateUpdate()
    {
        if (assignedIng != null)
        {
            if (assignedIng.cookedTime < assignedIng.cookingTime)
            {
                posSlider.value = assignedIng.cookedTime / assignedIng.cookingTime;
                posSliderImg.color = Color.Lerp(Color.yellow, Color.green, posSlider.value);
            }
            else if (assignedIng.cookedTime < assignedIng.cookingTime * 1.5)
            {
                // Doing Nothing
            }
            else if (assignedIng.cookedTime < assignedIng.cookingTime * 2.5)
            {
                posSlider.value = 1 - (assignedIng.cookedTime - assignedIng.cookingTime * 1.5f) / assignedIng.cookingTime;
                posSliderImg.color = Color.Lerp(Color.yellow, Color.green, posSlider.value);
            }
            else if (assignedIng.cookedTime < assignedIng.cookingTime * 3.5)
            {
                negSlider.value = (assignedIng.cookedTime - assignedIng.cookingTime * 2.5f) / assignedIng.cookingTime;
                negSliderImg.color = Color.Lerp(Color.yellow, Color.red, negSlider.value);
            }
            else
            {
                negSlider.value = 1;
                negSliderImg.color = Color.red;
            }
        }
    }
}
