using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Ability
{
    float cooldown = 5;
    float duration = 7;
    PlayerMovement movement;
    
    public override void initialize()
    {
        this.movement = GetComponent<PlayerMovement>();
        this.setCooldown(cooldown);
        this.setDuration(duration);
    }
    public override void startAbility()
    {
        Debug.Log("speedboost");
        movement.startAbility(Abilities.SpeedBoost);
    }
    public override void endAbility()
    {
        Debug.Log("end");
        movement.endAbility(Abilities.SpeedBoost);
    }
}
