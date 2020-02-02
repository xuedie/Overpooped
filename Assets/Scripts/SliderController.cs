using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    Slider[] sliders;
    [SerializeField]
    Text[] texts;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SyncroValue(float[] num)
    {
        sliders[0].value = num[0] / 100f;
        texts[0].text = (int)num[0] + "%";
        sliders[1].value = num[1] / 100f;
        texts[1].text = (int)num[1] + "%";
    }
}
