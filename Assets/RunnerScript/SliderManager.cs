using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour

{
    public Slider Slider;

    private void Update()
    {
        float sum;
        sum = 0;

        float i = sum / 200;
        if (Slider == null) return;
        Slider.value = i;
    }
}