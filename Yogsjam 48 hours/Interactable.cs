using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {

    }

    public void Add()
    {
        Game.instance.inter.interactables.Add(this.gameObject);
    }
    public void Remove()
    {
        Game.instance.inter.interactables.Remove(this.gameObject);
    }
}
