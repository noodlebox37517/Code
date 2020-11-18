using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : LaneReq
{

    public float cyclespeed = .5f;
    private Vector3[] lanes;
    public float cyclepos = 0;
    public int tarpos = 1;
    public int curpos = 0;
    public Vector2 tardir;
    private int sign =1;
    // Start is called before the first frame update
    public 
    void Start()
    {
        lanes = GameMaster.instance.GetComponent<InputController>().movePositions;

        tardir = (lanes[tarpos] - lanes[curpos]).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (cyclepos >= 1)
        {

            curpos = tarpos;
            if (tarpos + 1 >= lanes.Length)
            {   
                tarpos = 1;
                sign = -1;
            }
            else if (curpos == 0)
            {
                tarpos = 1;
                sign = 1;
            }
            else
            {
               
                tarpos +=sign;
            }
            tardir = ( lanes[tarpos] - lanes[curpos]).normalized;
            //update
            cyclepos = 0f;
        }
        else
        {
            //move towards tarpos
            cyclepos += cyclespeed * Time.deltaTime;
        }
        Vector2 pos = (Vector2)lanes[curpos] + (tardir * cyclepos);
        this.transform.position = new Vector3(pos.x,pos.y, this.transform.position.z);
        //update cycle pos
        //previospos+
        //update position

    }
    public override void Lane(int lane)
    {
        if (lane == 0)
        {
            curpos = 0;
            tarpos = 1;
        }
        else if(lane == 2)
        {
            curpos = 2;
            tarpos = 1;
        }
        //else default is middle
    }
}
