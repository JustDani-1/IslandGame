using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image QAbility;
    public Image EAbility;
    public void setText(float speed)
    {
        text.text = ((int)(speed * 10)).ToString();
    }

    public void setQFillAmount(float f)
    {
        
        QAbility.fillAmount = f;
    }

    public void setEFillAmount(float f)
    {
        EAbility.fillAmount = f;
    }
}
