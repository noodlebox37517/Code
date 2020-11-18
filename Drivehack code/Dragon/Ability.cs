using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Base")]
public class Ability : ScriptableObject
{
    public virtual bool AbilityUsable()
    {
        return false;
    }
    public virtual bool UseAbility()
    {

        return false;
    }
}
