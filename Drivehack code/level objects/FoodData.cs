using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : MonoBehaviour
{

    public bool cooked = false;
    public bool Burned = false;
    public bool chained = false;
    Material m_Material;



    public void Chain()
    {
        chained = true;
    }
    public int Value(int basevalue)
    {
        float tempval = basevalue;
        if (cooked)
        {
            tempval *= GameMaster.instance.GS.cookedMultiplyer;
        }
        return (int)tempval;
    }

    public void CheckLocal(bool contact)
    {
        if (contact)
        {
            if (chained)
            {
                //add to streak
                GameMaster.instance.CM.Chained(contact, gameObject);
            }
        }
        else
        {
            if (chained)
            {
                GameMaster.instance.CM.Chained(contact, gameObject);
            }
        }
    }
}
