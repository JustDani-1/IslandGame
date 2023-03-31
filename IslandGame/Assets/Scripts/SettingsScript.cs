using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider soundSlider;
    public Slider xSenSlider;
    public Slider ySenSlider;

    [Space]
    public Button DashButton;
    public Button DoubleJumpButton;
    public Button GlideButton;
    public Button SafetyIslandButton;
    public Button SpeedBoostButton;

    [Space]
    public TextMeshProUGUI QDebugText;
    public TextMeshProUGUI EDebugText;

    [Space]
    public Color defaultColor;
    public Color selectedColor;

    private bool changeQNext;

    private void Start()
    {
        if (PlayerPrefs.GetInt("settings") == 1)
        {
            loadSettings();
        }
        else 
        {
            defaultSettings();
            PlayerPrefs.SetInt("settings", 1);
            PlayerPrefs.Save();
            loadSettings();
        }
    }

    public void setSound()
    {
        DataManager.Instance.sound = soundSlider.value;
        PlayerPrefs.SetFloat("sound", DataManager.Instance.sound);
    }
    public void setXSen()
    {
        DataManager.Instance.xSen = xSenSlider.value * 10;
        PlayerPrefs.SetFloat("xSen", DataManager.Instance.xSen);
    }
    public void setYSen()
    {
        DataManager.Instance.ySen = ySenSlider.value * 10;
        PlayerPrefs.SetFloat("ySen", DataManager.Instance.ySen);
    }

    public void handleAbilities(Button presser)
    {
        if (EnumToButton(DataManager.Instance.QAbility).Equals(presser)
            || EnumToButton(DataManager.Instance.EAbility).Equals(presser))
        {
            //switch abilities
            Abilities q = DataManager.Instance.QAbility;
            Abilities e = DataManager.Instance.EAbility;
            DataManager.Instance.QAbility = e;
            DataManager.Instance.EAbility = q;
            PlayerPrefs.SetInt("QAbility", (int)DataManager.Instance.QAbility);
            PlayerPrefs.SetInt("EAbility", (int)DataManager.Instance.EAbility);
            UpdateDebugText();
            //UpdateAllButtonColor();
            return;
        }

        if (changeQNext)
        {
            DataManager.Instance.QAbility = ButtonToAbility(presser);

        }
        else 
        {
            DataManager.Instance.EAbility = ButtonToAbility(presser);
        }
        changeQNext = !changeQNext;
        PlayerPrefs.SetInt("QAbility", (int)DataManager.Instance.QAbility);
        PlayerPrefs.SetInt("EAbility", (int)DataManager.Instance.EAbility);
        UpdateDebugText();
        //UpdateAllButtonColor();
        PlayerPrefs.Save();

    }
    private void UpdateDebugText() 
    {
        QDebugText.text = $"Q: {DataManager.Instance.QAbility.ToString()}";
        EDebugText.text = $"E: {DataManager.Instance.EAbility.ToString()}";
    }
    private void UpdateAllButtonColor() 
    {
        //long but can't care enough for a better solution

        setButtonColor(DashButton, defaultColor);
        setButtonColor(DoubleJumpButton, defaultColor);
        setButtonColor(GlideButton, defaultColor);
        setButtonColor(SafetyIslandButton, defaultColor);
        setButtonColor(SpeedBoostButton, defaultColor);

        Abilities q = DataManager.Instance.QAbility;
        Abilities e = DataManager.Instance.EAbility;
        setButtonColor(EnumToButton(q), selectedColor);
        setButtonColor(EnumToButton(e), selectedColor);
}
    private void setButtonColor(Button b, Color c) 
    {
        ColorBlock block = b.colors;
        block.normalColor = c;
        b.colors = block;
    }

    public void loadSettings()
    {
        
        DataManager.Instance.sound = PlayerPrefs.GetFloat("sound");
        soundSlider.value = PlayerPrefs.GetFloat("sound");

        DataManager.Instance.xSen = PlayerPrefs.GetFloat("xSen");
        xSenSlider.value = PlayerPrefs.GetFloat("xSen");

        DataManager.Instance.ySen = PlayerPrefs.GetFloat("ySen");
        ySenSlider.value = PlayerPrefs.GetFloat("ySen");

        DataManager.Instance.QAbility = (Abilities)(PlayerPrefs.GetInt("QAbility"));
        DataManager.Instance.EAbility = (Abilities)(PlayerPrefs.GetInt("EAbility"));

        UpdateDebugText();
        //UpdateAllButtonColor();
    }

    public void defaultSettings()
    {
        PlayerPrefs.SetInt("QAbility", (int)(Abilities.SafetyIsland));
        PlayerPrefs.SetInt("EAbility", (int)(Abilities.SpeedBoost));
        PlayerPrefs.SetFloat("sound", 1f);
        PlayerPrefs.SetFloat("xSen", 5f);
        PlayerPrefs.SetFloat("ySen", 5f);

        PlayerPrefs.Save();
    }

    private Button EnumToButton(Abilities a)
    {
        switch (a)
        {
            case Abilities.Dash:
                return DashButton;
            case Abilities.DoubleJump:
                return DoubleJumpButton;
            case Abilities.Glide:
                return GlideButton;
            case Abilities.SafetyIsland:
                return SafetyIslandButton;
            case Abilities.SpeedBoost:
                return SpeedBoostButton;
        }
        return null;
    }

    private Abilities ButtonToAbility(Button b) 
    {
        return b.GetComponent<MarkButtonWithAbility>().ability;
    }
}
