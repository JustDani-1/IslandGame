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

    public Button DashButton;
    public Button DoubleJumpButton;
    public Button GlideButton;
    public Button SafetyIslandButton;
    public Button SpeedBoostButton;

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
        
        switchButtonColor(presser);
        
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
        switchButtonColor(enumToButton(DataManager.Instance.QAbility));

        DataManager.Instance.EAbility = (Abilities)(PlayerPrefs.GetInt("EAbility"));
        switchButtonColor(enumToButton(DataManager.Instance.EAbility));
        Debug.Log(DataManager.Instance.EAbility);


        
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

    public void switchButtonColor(Button b)
    {
        ColorBlock colors = b.colors;

        if(colors.normalColor == Color.grey)
        {
            colors.normalColor = Color.white;
        }
        else
        {
            colors.normalColor = Color.grey;
        }
        b.colors = colors;
    }

    public Button enumToButton(Abilities e)
    {
        switch (e)
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


}
