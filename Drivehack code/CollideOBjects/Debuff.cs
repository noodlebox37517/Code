using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeBuff", menuName = "Collidables/DeBuff")]
public class Debuff : Collidable
{
    public bool Blind = false;
    public bool Invischar = false;
    public bool slow = false;
    public float timer = 5f;
    public override void Contact()
    {
        if (Blind)
        {
            GameMaster.instance.Effects.Blind();
        }
        if (Invischar)
        {
           
        }
        if (slow)
        {
            GameMaster.instance.Effects.StartSlow(timer);
        }
    }
    public override void setup(GameObject GO)
    {
        base.setup(GO);
        //apply special conditions


    }
}
