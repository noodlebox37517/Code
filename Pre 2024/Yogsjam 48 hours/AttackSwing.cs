using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwing : MonoBehaviour
{
    public bool aoe;
    public HitModel hit;
    public int damage = 0 ;
    public float delaytime= 0f;
    public string targetTag;
    protected float timealive;
    public virtual void Update()
    {
        if (Time.time >= timealive + delaytime)
        {
            Destroy(this.gameObject);
        }
    }
    public void init(int dmg, float delay, string tag, HitModel hitmod)
    {
        timealive = Time.time;
        hit = hitmod;
        damage = dmg;
        delaytime = delay;
        targetTag = tag;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(targetTag +"--" + other.tag);

        if (other.tag == targetTag || aoe)
        {
            
            hit.Hit(other.gameObject, transform.position);
            
        }

    }
}
