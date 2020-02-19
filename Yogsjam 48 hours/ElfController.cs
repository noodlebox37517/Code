using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfController : Interactable  
{
    public bool wander = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Building").GetComponent<Building>().workingelves.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(wander == true)
        {
            //Listen for whack/slap
        }
    }
    public void Wander()
    {
        transform.Rotate(-90f,0f,0f);
        wander = true;
        Game.instance.inter.interactables.Add(this.gameObject);
    }
    public void Work()
    {
        transform.Rotate(90f, 0f, 0f);
        wander = false;
        Game.instance.inter.interactables.Remove(this.gameObject);
        GameObject.FindGameObjectWithTag("Building").GetComponent<Building>().workingelves.Add(this.gameObject);
    }

    public override void Interact()
    {
        Work();
    }
}
