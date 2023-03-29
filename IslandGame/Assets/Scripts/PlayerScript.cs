using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
//using UnityEditor.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Ability QAbility;
    Ability EAbility;
    private UIManager uimanager;
    private Rigidbody rb;
    private GameManagerScript gameManager;
    float runTime = 0;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        QAbility = this.GetComponent<DoubleJump>();
        EAbility = this.GetComponent<Glide>();
        uimanager = GameObject.FindObjectOfType<UIManager>();
        gameManager = GameObject.FindObjectOfType<GameManagerScript>();
        QAbility.initialize();
        EAbility.initialize();

    }

    // Update is called once per frame
    void Update()
    {
        QAbility.doCooldown(Time.deltaTime);
        EAbility.doCooldown(Time.deltaTime);
        runTime += Time.deltaTime;
        uimanager.setQFillAmount(QAbility.getFillAmount());
        uimanager.setEFillAmount(EAbility.getFillAmount());
        uimanager.setTimeText(runTime);

        if (Input.GetKey(KeyCode.Q))
        {
            QAbility.ButtonPressed();
        }
        if (Input.GetKey(KeyCode.E))
        {
            EAbility.ButtonPressed();
        }
        if (Input.GetKey(KeyCode.R))
        {
            runTime = 0;
            QAbility.resetAbility();
            EAbility.resetAbility();
            transform.position = FindObjectOfType<spawnPoint>().transform.position;
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uimanager.pauseMenuChange();
        }

        updateUI();

    }
    void updateUI()
    {
        Vector3 v = rb.velocity;
        //v.y = 0;
        float speed = v.magnitude;
        uimanager.setSpeedText(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flag"))
        {
            other.GetComponent<Confetti>().makeConfetti();
            StartCoroutine(wait(3));
        }
    }
    private IEnumerator wait(float t) 
    {
        uimanager.pauseTimer();
        yield return new WaitForSeconds(t);
        gameManager.resetGame();
    }
}


