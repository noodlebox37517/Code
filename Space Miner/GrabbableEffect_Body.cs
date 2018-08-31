using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableEffect_Body : Grabbable_Body {
    public enum Effect {Explosion = 0 , Enrichement = 1 , ScanPulse = 2 , MassChange = 3 , SpatialAnomaly = 4}
    public Effect fect = Effect.Explosion;
    public bool effectontouch = false;
    public int effectID = 0;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Activate(Effect ef)
    {

        switch (ef)
        {
            case Effect.Explosion:
                Explode(5, effectID, transform.position);
                //Destroy(this.gameObject);
                break;
            case Effect.Enrichement:
                Explode(5, effectID, transform.position);
                //Destroy(this.gameObject);
                break;
            default:
                Debug.Log("effect not found");

                break;
        }
    }

    public void Explode( float radius, int explosionID, Vector3 spawnpos)
    {
        Master.instance.LoadedLevel.GetComponent<RoidCounter>().bombCount++;
        //get explosion object
        GameObject tempexplode = Master.instance.sources.Explosions[explosionID];
        tempexplode = Instantiate<GameObject>(tempexplode,spawnpos,Quaternion.identity);
        base.RemoveSelf();

    }

    public override void Grabbed(Hook hk)
    {
        
        //base.Grabbed(hk);
        if (effectontouch)
        {
            Activate(fect);
        }
        //hk.MaxRange();
    }
    public override void Shatter()
    {
        if (effectontouch)
        {
            Activate(fect);
        }
        //remove from map list
        base.RemoveSelf();
    }
}
