using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

//handles the player ui like abilities, or the time
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI seedText;
    public TextMeshProUGUI timeText;
    public Image QAbility;
    public Image EAbility;
    public GameObject pauseMenu;

    private bool paused = false;


    public void setSpeedText(float speed)
    {
        speedText.text = ((int)(speed * 10)).ToString();
    }
    public void setTimeText(float time)
    {
        if (!paused)
        {
            timeText.text = time.ToString("F4");
        }
    }
    public void pauseTimer()
    {
        paused = true;
    }

    public void setQFillAmount(float f)
    {

        QAbility.fillAmount = f;
    }

    public void setEFillAmount(float f)
    {
        EAbility.fillAmount = f;
    }

    public void pauseMenuChange()
    {
        seedText.text = DataManager.Instance.seed.ToString();
        if (pauseMenu.activeSelf)
        {
            resume();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0; // makes the game pasuse
            pauseMenu.SetActive(true);
        }

    }

    public void copyToClipboard()
    {
        GUIUtility.systemCopyBuffer = seedText.text;
    }

    public void resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1; //unpause
        pauseMenu.SetActive(false);
    }
}
