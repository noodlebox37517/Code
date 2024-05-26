using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : AttackSwing
{
    public float speed  = 1f;
    public float fly = 1f;
    public float droprate = 2f;
    public bool penetration = false;
    public void Launch(float spd, float flytime)
    {
        speed = spd;
        fly = flytime;
    }
    public override void Update()
    {
        
        transform.position += transform.forward * (speed*Time.deltaTime);
        if (Time.time >= timealive+fly)
        {

            transform.position -= new Vector3(0f, droprate*((Time.time - (timealive + fly))/fly) * Time.deltaTime, 0f);
            if (transform.position.y<= 0f)
            {
                Destroy(this.gameObject);
            }
        }
        base.Update();
    }
    public override  void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            base.OnTriggerEnter(other);

        }
        if (!penetration || other.tag !=this.gameObject.tag)
            Destroy(this.gameObject);

    }
}
