using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoubleJump : Ability
{
    float cooldown = 5;
    PlayerMovement movement;
    
    public override void initialize()
    {
        this.movement = GetComponent<PlayerMovement>();
        this.setCooldown(cooldown);
    }
    public override void startAbility()
    {
        Debug.Log("doublejump");
        movement.Jump();
        
    }

    public override void endAbility()
    {
    }


}
