using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Buff", menuName = "Collidables/Buff")]
public class Buff : Collidable
{
    public bool Magnet = false;
    public bool shield = false;
    public bool life = false;
    public override void Contact()
    {
        if (Magnet)
        {
            GameMaster.instance.dragon.GetComponent<Magnet>().StartMagnet();
        }
        if (shield)
        {
            GameMaster.instance.dragon.shield++;
        }
        if (life)
        {
            GameMaster.instance.life.LifeAdd(1);
        }
    }
    public override void setup(GameObject GO)
    {
        base.setup(GO);
        //apply special conditions


    }
}
