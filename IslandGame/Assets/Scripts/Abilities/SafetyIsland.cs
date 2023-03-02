using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyIsland : Ability
{
    float cooldown = 10;
    public GameObject prefab;
    
    public override void initialize()
    {
        this.setCooldown(cooldown);
    }
    public override void startAbility()
    {
        Vector3 pos = transform.position;
        pos.y -= 2;
        GameObject g = Instantiate(prefab, pos, Quaternion.identity);
        g.transform.Rotate(0,transform.localEulerAngles.y,0);
    }

    public override void endAbility()
    {
    }


}
