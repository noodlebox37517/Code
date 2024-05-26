using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DrainHazard", menuName = "Collidables/DrainHazard")]
public class EnergyHazard : Hazard
{
    public int drainamount = 50;

    public override void Contact()
    {
        GameMaster.instance.dragon.TakeEnergy(drainamount);
    }

    }
