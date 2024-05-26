using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool alive = true;
    public float CurrentHealth = 1;
    public int maxHealth=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Debug.Log("we died");
        this.GetComponent<Charecter>().Death();
    }
    public void Damage(int dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Damage( float dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int heal)
    {
        CurrentHealth += heal;
        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }
}
