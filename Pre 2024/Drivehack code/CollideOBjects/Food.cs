using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "coin", menuName = "Collidables/coin")]
public class Food : Collidable
{
    public bool Cookable = false;
    public bool Burnable = false;
    public bool random = false;
    public int min = 1;
    public bool Chain = false;

    public int Bonus = 0;
    public int chainLnegth;
    public bool stealth = false;
    public Material stealthMat;
    public int foodAmount = 0;

    public override void Contact()
    {
        int food = foodAmount;
        if (random)
        {
            food = Random.Range(min, foodAmount);
            //add random effect que
        }
       
        GameMaster.instance.LC.FoodEaten(food);
    }
    public override void Missed(GameObject GO)
    {
        GO.GetComponent<FoodData>().CheckLocal(false);
        GameMaster.instance.LC.StreakReset();
    }
    public override void setup(GameObject GO)
    {
        if (stealth)
        {
          Stealth stel =  GO.AddComponent<Stealth>();
            stel.invismat = stealthMat;
            stel.GoDark();

        }
        if (Chain)
        {
            if (!GameMaster.instance.CM.active)
            {
                GameMaster.instance.CM.ChainStart(chainLnegth, Bonus, GO);
                if(GO.GetComponent<FoodData>()!= null)
                GO.GetComponent<FoodData>().chained = true;
            }
            //GameMaster.instance.CM.ChainRequest(GO, this);
            // ask chainmanger
            //if false do not add chain script to gameobject
        }
        else if (!Special())
        {
            if (GameMaster.instance.CM.ChainRequest(GO))
            {
                GO.GetComponent<FoodData>().Chain();
            }
        }
        base.setup(GO);
        //apply special conditions


    }
    public bool Special()
    {
        if (stealth||Chain)
        {
            return true;
        }
        return false;
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
            case 2:
                if (Cookable)
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
