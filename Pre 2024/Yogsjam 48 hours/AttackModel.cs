using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "melee", menuName = "Player/Attack")]
public class AttackModel : ScriptableObject
{
    public bool duals = false;
    public HitModel hit;
    public GameObject SantaPrefab;
    public float CD =.5f;
    public GameObject Swingobj;
    public int dmg = 1;
    public float delay = 1f;
    public string tag;
    public Vector3 spawnoffset;
    public float offsetmag = 2;
    public virtual void Attack(GameObject attacker, Transform spawnpoint, Vector3 dir)
    {
        GameObject swing = Instantiate<GameObject>(Swingobj,(spawnpoint.position),attacker.transform.rotation);
        swing.transform.rotation = Quaternion.LookRotation(dir);
        swing.GetComponent<AttackSwing>().init(dmg,delay,tag, hit);
        //spawn attack mesh
        // give dmg, delay time, target tag/type
    }
}
