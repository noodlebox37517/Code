using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hazard", menuName = "Collidables/BlindHazard")]
public class BlindHazard : Hazard
{
    public float blindtime = 0f;
    // Start is called before the first frame update
    public override void Contact()
    {       
     
        Debug.Log(this.name + " HIT");
        //temp
        if (!GameMaster.instance.dragon.invulnerable)
        {
            //TODO replace with death script/prompt
            //GameMaster.instance.dragon.Damage();
            GameMaster.instance.Effects.Blind();
        }
            
    }
}
