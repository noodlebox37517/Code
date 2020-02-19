using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HIT", menuName = "Player/HIT")]
public class HitModel : ScriptableObject
{
    public float force =100;
    public int damage;
    public GameObject obj;
    public float time = 3f; 
    public int Hittype = 0;
    public float explosionradius= 5;
public virtual void Hit(GameObject hitobject, Vector3 hitpos)
    {

        switch(Hittype)
        {
            case 0:
                if (hitobject.GetComponent<Health>().alive)
                {
                    hitobject.GetComponent<Health>().Damage(damage);
                }
                else
                {
                    Vector3 dir = hitpos - hitobject.transform.position;
                    dir.Normalize();
                    hitobject.GetComponent<Rigidbody>().AddForce(-dir * force,ForceMode.Impulse);
                    //hitobject.GetComponent<Rigidbody>().AddExplosionForce(force, hitpos, 3f, 2f, ForceMode.Impulse);
                }
                break;

            case 1:
                //Explosion
                GameObject temp = Instantiate<GameObject>(obj, hitpos, Quaternion.identity);
                temp.GetComponent<Explosion>().dmg = damage;
                temp.GetComponent<Explosion>().lingertime = time;
                
                //spawn obj
                //spawn effect
                if (hitobject.GetComponent<Health>().alive)
                {
                }
                else
                {
                    
                }
                    break;
            default:
                hitobject.GetComponent<Health>().Damage(damage);
                break;

        }

    }
}
