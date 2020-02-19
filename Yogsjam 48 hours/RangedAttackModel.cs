using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ranged", menuName = "Player/RAttack")]
public class RangedAttackModel : AttackModel
{
    
    public float bulletspeed = 1f;
    public float flytime = 5f;
    public override void Attack(GameObject attacker, Transform spawnpoint, Vector3 dir)
    {

        GameObject swing = Instantiate<GameObject>(Swingobj, spawnpoint.position, attacker.transform.rotation);
        swing.transform.rotation = Quaternion.LookRotation(dir);
        swing.GetComponent<AttackBullet>().init(dmg, delay, tag, hit);
        swing.GetComponent<AttackBullet>().Launch(bulletspeed, flytime);
        //spawn attack mesh
        // give dmg, delay time, target tag/type
    }
}
