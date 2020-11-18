using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(fileName = "PowerUp", menuName = "Collidables/PowerUp")]
public class PowerUp : Collidable
{
    public enum powerType {Extralife =0, Energy=1,Multi=2, Immune=3 }
    public powerType pt;
    public int amount = 0;
    public override void Contact()
    {
        //apply power up
        switch(pt)
        {
            case powerType.Extralife:
                GameMaster.instance.life.LifeAdd(amount);
                break;
            case powerType.Energy:
                GameMaster.instance.dragon.TakeEnergy(-amount);
                break;
            case powerType.Multi:
                GameMaster.instance.LC.Streaks += amount;
                break;
            case powerType.Immune:
                GameMaster.instance.dragon.Invulnerable(amount);
                break;

            default:
                Debug.Log("exc no powertype detected");
                break;

        }
    }

    }
