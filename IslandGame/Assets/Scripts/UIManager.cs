using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void setText(float speed)
    {
        text.text = ((int)(speed*10)).ToString();
    }
}
