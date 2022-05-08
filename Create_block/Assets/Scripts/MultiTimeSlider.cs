using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiTimeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime()
    {
        text.text = slider.value.ToString() + "min";
    }
}
