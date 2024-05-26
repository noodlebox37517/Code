using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaWepUnlock : Interactable
{

    void Start()
    {
        Add();
    }
    public override void Interact()
    {
        Game.instance.GetComponent<Weapons>().UnlockWeaponRandom();
        Remove();
        Destroy(gameObject);
    }
}
