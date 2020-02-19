using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : Interactable
{
    public Collider col;

    private void Start()
    {
        Game.instance.CV.GetComponent<ImageShower>().images[4].gameObject.SetActive(true);
        Add();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        //check if holding present
    //        if (other.GetComponent<Santa>().carrying == null)
    //        {
    //            col.enabled = false;
    //            other.GetComponent<Santa>().Pickup(this.gameObject);

    //        }
    //    }
    //}
    public override void Interact()
    {
       Game.instance.santa.Pickup(this.gameObject);
        Game.instance.CV.GetComponent<ImageShower>().images[4].gameObject.SetActive(false);
        Game.instance.playertarget = Game.instance.curdelgo;

    }
}
