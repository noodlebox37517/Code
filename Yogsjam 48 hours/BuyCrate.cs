using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCrate : Interactable
{
    public Buyable salemodel;
    // Start is called before the first frame update
    void Start()
    {
        Add();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact()
    {
        if (Game.instance.santa.carrying == null)
        {
            if (Game.instance.Resource > salemodel.Buycost) {
                GameObject tempGO = Instantiate<GameObject>(salemodel.crate);
                if (Game.instance.santa.Pickup(tempGO))
                {
                    tempGO.GetComponent<Crate>().contains = salemodel.defence;
                    Game.instance.Resource -= salemodel.Buycost;
                }
            }
        }

    }

}
