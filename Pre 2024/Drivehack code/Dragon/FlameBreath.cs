using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability", menuName = "Ability/FlameBreath")]
public class FlameBreath : Ability
{
    public float flameRange = 1f;
    public int energyCost = 100;
    public float upTime = 1f;
    public GameObject flamefab;

    public override bool AbilityUsable()
    {
        //draw ray from dragon pos fwd set distance or create object for length of breath
        if (GameMaster.instance.dragon.Energy>=energyCost)
        {
            return true;
        }
        return base.AbilityUsable();
    }

    public override bool UseAbility()
    {
        //check if cd off
        if (GameMaster.instance.dragon.UseEnergy(energyCost))
        {
            //spawn flame on dragon point + offset
          GameObject temp =  Instantiate<GameObject>(flamefab);
            temp.GetComponent<Flame>().Time = upTime;
        }
        return base.UseAbility();
    }
}
