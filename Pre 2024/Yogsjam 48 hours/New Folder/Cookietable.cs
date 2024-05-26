using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookietable : Interactable


{
    public int health =30;
    public int nextcookies = 0;
    public int max = 5;
    public float cookieregentime = 5;
    private float lastregen = 0f;

    public List<GameObject> cookiegos;
    public void primetable()
    {
        max = cookiegos.Count;
        nextcookies = max - 1;
    }
    private void Start()
    {
        
        Add();

    }
    private void Update()
    {
        if(Time.time> lastregen+ cookieregentime)
        {
            if(nextcookies< cookiegos.Count-1)
            {
                nextcookies++;
                cookiegos[nextcookies].SetActive(true);
                lastregen = Time.time;
            }
        }
        
    }
    public override void Interact()
    {
        if(nextcookies == cookiegos.Count - 1)
        {
            lastregen = Time.time;
        }
        if (nextcookies > -1)
        {
            if (Game.instance.santa.GetComponent<Health>().CurrentHealth != Game.instance.santa.GetComponent<Health>().maxHealth) {
                cookiegos[nextcookies].SetActive(false);
                nextcookies--;
                Game.instance.santa.GetComponent<Health>().Heal(health);
            }

        }
          


    }

}
