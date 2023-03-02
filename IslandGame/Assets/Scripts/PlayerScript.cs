using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Ability ability1;
    Ability ability2;
    void Start()
    {
        ability1 = this.GetComponent<DoubleJump>();
        ability2 = this.GetComponent<SafetyIsland>();
        ability1.initialize();
        ability2.initialize();
    }

    // Update is called once per frame
    void Update()
    {
        ability1.doCooldown(Time.deltaTime);
        ability2.doCooldown(Time.deltaTime);

        if(Input.GetKey(KeyCode.Q))
        {
            ability1.ButtonPressed();
        }
        if (Input.GetKey(KeyCode.E))
        {
            ability2.ButtonPressed();
        }
    }

}


