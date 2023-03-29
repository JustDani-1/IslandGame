using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities 
{
    DoubleJump,
    SpeedBoost,
    Glide,
    Dash,
    SafetyIsland
}

public abstract class Ability : MonoBehaviour
{
    private bool isReady = true;
    private bool isActive = false;
    float _cooldown;
    float _duration = 0;
    float cooldownTimer = 0;
    float durationTimer = 0;

    

    public void setCooldown(float cd)
    {
        _cooldown = cd;
    }
    public void setDuration(float d)
    {
        _duration = d;
    }

    public void resetAbility()
    {
        isReady = true;
        cooldownTimer = _cooldown;
        durationTimer = 0;
        endAbility();
    }
    public abstract void initialize();
    public void ButtonPressed()
    {
        if(isReady)
        {
            isReady = false;
            isActive = true;
            cooldownTimer = 0;
            startAbility();
        }
    }

    public abstract void startAbility();

    public abstract void endAbility();


    public void doCooldown(float dt)
    {
        if(isActive)
        {
            cooldownTimer = 0;
            if(durationTimer < _duration)
            {
                durationTimer += dt;
            }
            else
            {
                endAbility();
                durationTimer = 0;
                isActive = false;
            }
        }
        else
        {
            if (cooldownTimer < _cooldown)
            {
                cooldownTimer += dt;
            }
            else
            {
                isReady = true;
            }
        }
        
    }

    public float getFillAmount()
    {
        if(isReady)
        {
            return 1;
        }
        if(isActive)
        {
            return 0;

        }
        return cooldownTimer / _cooldown;
    }
}
