using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public Collidable Data;
    /// <summary>
    /// when true deletes object when ebhind camera , wil check everyframe
    /// </summary>
    public bool deleteOffScreen = true;
    public bool tracked = true;

    private void Start()
    {
        //Data.setup(this.gameObject);
        if (tracked)
        {
            GameMaster.instance.GetComponent<TerrainController>().levelobjects.Add(this.gameObject);
        }
    }

    private void Update()
    {
        if (deleteOffScreen)
        {
            //check if behind camera
            if (transform.position.z < GameMaster.instance.cam.transform.position.z)
            {
                Data.Missed(this.gameObject);
                DestroySelf();
            }
        }
    }
    public virtual void Lane(int lane)
    {
        if (this.GetComponent<LaneReq>() != null)
        {
            this.GetComponent<LaneReq>().Lane(lane);
        }
    }
    public void Contact()
    {
        Data.Contact();
        if (Data.effectOnPickup)
        {
            //spawn effect
            Instantiate<GameObject>(Data.particleEffect,this.transform.position,Quaternion.identity);
        }
    }
    public void DestroySelf()
    {
        if (tracked)
        {
            GameMaster.instance.GetComponent<TerrainController>().levelobjects.Remove(this.gameObject);
        }
        Destroy(this.gameObject);
    }

}
