using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Ability QAbility;
    Ability EAbility;
    private UIManager uimanager;
    private Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        QAbility = this.GetComponent<DoubleJump>();
        EAbility = this.GetComponent<SafetyIsland>();
        uimanager = GameObject.FindObjectOfType<UIManager>();
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

        }
    }
}


