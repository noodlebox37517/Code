using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic", menuName = "Collidables/Generic")]
public class Collidable : ScriptableObject
{
    public GameObject prefab;
    public bool effectOnPickup = false;
    public GameObject particleEffect;
    public virtual void Contact()
    {
        Debug.Log(this.name+" collide");

    }
    public virtual bool Contact(int id)
    {
        return false;
    }
    public virtual void Missed(GameObject GO)
    {

    }
    public virtual void setup(GameObject GO)
    {
        GO.GetComponent<CollidableObject>().Data = this;
        GO.name = this.name;

    }
    
}
