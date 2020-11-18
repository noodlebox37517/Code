using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hazard", menuName = "Collidables/Hazard")]
public class Hazard : Collidable
{
    public bool Delayed = false;
    public float revealDist = 12f;
    public bool moving = false;
    public bool phasing = false;
    public float cycleSpeed = 0.5f;
    public bool Burnable = false;
    public override void Contact()
    {
        Debug.Log(this.name + " HIT");
        //temp
        if (!GameMaster.instance.dragon.invulnerable)
            //TODO replace with death script/prompt
            GameMaster.instance.dragon.Damage();
            
    }
    public override void setup(GameObject GO)
    {
        if (Delayed)
        {
            if (GO.GetComponent<Stealth>()!= null)
            GO.GetComponent<Stealth>().revealDistance = revealDist;
        }
        if (phasing)
        {
          Phasing phas =  GO.AddComponent<Phasing>();
            phas.cyclespeed = cycleSpeed;
        }
        base.setup(GO);
        //apply special conditions


    }
    public override bool Contact(int id)
    {
        switch (id)
        {
            
            case 1:
                if (Burnable)
                {
                    return true;
                }
                break;

            default:
                break;
                
        }
        return false;
    }

}
