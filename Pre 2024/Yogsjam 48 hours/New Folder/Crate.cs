using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Placable
{
    public Defence contains;
    private Vector3 placementposition;
        public Vector3 rayoffset = new Vector3( 0f,1f,0f);
    public Vector3 Direction;
    public GameObject shade;
    public shadechecker checker ;
    public bool canplace = true;
    // Start is called before the first frame update
    void Start()
    {
        shade = Instantiate<GameObject>(contains.ShadowObject, Getterrainpos(), Game.instance.santa.transform.rotation);
        checker = shade.GetComponent<shadechecker>();
    }

    // Update is called once per frame
    void Update()
    {
        // draw ray
        //Debug.Log(gameObject.name + " HERE");
        placementposition = Getterrainpos();
        
        shade.transform.position = placementposition;
        shade.transform.rotation = Game.instance.santa.transform.rotation;

        //adcheck for collision
        canplace = checker.clear;


        //draw shadow object
    }
    public override bool Place()
    {
        //place conaints.prefab
        if (canplace)
        {
            
            Game.instance.defs.Active.Add(Instantiate<GameObject>(contains.prefab, Getterrainpos(), Game.instance.santa.transform.rotation));
            Destroy(shade);
            Destroy(this.gameObject);
            return true;


        }
        return false;
    }
    public Vector3 Getterrainpos()
    {
        Vector3 val = new Vector3();
        int layerMask = 1 << 8;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
       Vector3 tempDirection = Game.instance.santa.carrypos.forward*Direction.z + -Game.instance.santa.carrypos.up*Direction.y;
        tempDirection.Normalize();
        

        if (Physics.Raycast(Game.instance.santa.carrypos.position, tempDirection, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(Game.instance.santa.carrypos.position, tempDirection * hit.distance, Color.magenta);
            //Debug.Log("Did Hit");
            val = hit.point;
        }
        //draw ray from mousepos on screen 
        // to ground forward from screen
        return val;
    }
}
