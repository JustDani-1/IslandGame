using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider soundSlider;
    public Slider xSenSlider;
    public Slider ySenSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSound()
    {
        DataManager.Instance.sound = soundSlider.value;
    }
    public void setXSen()
    {
        DataManager.Instance.xSen = xSenSlider.value * 10;
    }
    public void setYSen()
    {
        DataManager.Instance.ySen = ySenSlider.value * 10;
    }
}
