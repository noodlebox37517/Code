using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Base")]
public class EnemyModel : ScriptableObject
{
    public float Speed;
    public float chaserange=50f;
    public float AttackRange;
    public float attackCD;
    public int  attackDamage;
    public float StandoffRange;
    public int MaxHealth;
    public AttackModel Attackmodel;


     public virtual void Attack(GameObject attacker,Transform spawnpoint,GameObject target)
    {
        if(target.GetComponent<Health>()!= null)
        {
            if (Attackmodel != null)
            {
                Vector3 dir = -(spawnpoint.position - (target.transform.position+Vector3.up)).normalized;
                Attackmodel.Attack(attacker, spawnpoint, dir);
            }
            else
            {
                target.GetComponent<Health>().Damage(attackDamage);
            }
        }
    }
}
