using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float force;
    public bool hitmines = true;
    public string targetTag = "Enemy";
    public int dmg = 1;
    public float lingertime = 3;
    private float spawntime;
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            
            other.GetComponent<Health>().Damage(dmg);
            other.GetComponent<Rigidbody>().AddExplosionForce(force, this.transform.position, 3f, 2f,ForceMode.Impulse);

        }

        if(hitmines && other.tag == "Mine")
        {
            other.GetComponent<Mine>().Explode();
        }

    }
    private void Start()
    {
        spawntime = Time.time;   
    }
    private void Update()
    {
        if(Time.time> spawntime + lingertime)
        {
            Destroy(this.gameObject);
        }
    }
}
