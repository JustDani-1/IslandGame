using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    float cooldown = 3.5f;
    float duration = 0.2f;
    PlayerMovement movement;

    public override void initialize()
    {
        this.movement = GetComponent<PlayerMovement>();
        this.setCooldown(cooldown);
        this.setDuration(duration);
    }
    public override void startAbility()
    {
        Debug.Log("dash");
        movement.dash();
    }
    public override void endAbility()
    {
        Debug.Log("end");
        movement.stopDash();
    }
}
