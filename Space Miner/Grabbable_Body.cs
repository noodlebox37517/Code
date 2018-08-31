using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script to allow body to be grabbed and used by Hook script.
/// </summary>
public class Grabbable_Body : Body {

    public int grabValue = 0;
    public float mass =1f; 
    public bool grabbed = false;
    public Hook hook ;
    public GameObject releaseEffect;
    // Use this for initialization
    public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
        
	}
    public override void RemoveSelf()
    {
        //remove from map list\
        if(Master.instance.LoadedLevel.GetComponent<Map>() != null)
        Master.instance.LoadedLevel.GetComponent<Map>().LiveBodies.Remove(this.gameObject);
        base.RemoveSelf();
    }
    public override void Shatter()
    {
        RemoveSelf();
    }

    public virtual void Grabbed(Hook hk)
    {
        grabbed = true;
        hook = hk;
    }
}
