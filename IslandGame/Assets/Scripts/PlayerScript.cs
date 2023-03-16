using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Ability QAbility;
    Ability EAbility;
    private UIManager uimanager;
    private Rigidbody rb;
    private GameManagerScript gameManager;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        QAbility = this.GetComponent<DoubleJump>();
        EAbility = this.GetComponent<SpeedBoost>();
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
        uimanager.setQFillAmount(QAbility.getFillAmount());
        uimanager.setEFillAmount(EAbility.getFillAmount());

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
            transform.position = FindObjectOfType<spawnPoint>().transform.position;
        }

        updateUI();

    }
    void updateUI() 
    {
        Vector3 v = rb.velocity;
        //v.y = 0;
        float speed = v.magnitude;
        uimanager.setText(speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("flag"))
        {
            gameManager.resetGame();
        }
    }
}


