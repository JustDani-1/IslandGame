using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : Ability
{
    float cooldown = 6;
    float duration = 2;
    PlayerMovement movement;

    public override void initialize()
    {
        this.movement = GetComponent<PlayerMovement>();
        this.setCooldown(cooldown);
        this.setDuration(duration);
    }
    public override void startAbility()
    {
        Debug.Log("glide");
        movement.startAbility(Abilities.Glide);
    }
    public override void endAbility()
    {
        Debug.Log("end");
        movement.endAbility(Abilities.Glide);
    }
}
