using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyIsland : Ability
{
    float cooldown = 7;
    public GameObject prefab;
    public GameObject g;


    public override void initialize()
    {
        this.setCooldown(cooldown);
    }
    public override void startAbility()
    {
        Vector3 pos = transform.position;
        pos.y -= 7;
        if(g != null)
        {
            Destroy(g);
        }
        g = Instantiate(prefab, pos, Quaternion.identity);
        g.transform.Rotate(0,transform.localEulerAngles.y,0);
    }

    public override void endAbility()
    {

    }


}
