using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject mfield;
    public bool on = false;
    public float range = 4f;
    public float duration= 6f;
    private float timer=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.LC.LevelPlay)
        {
           if( GameMaster.instance.LC.levelTime >= timer)
            {
                //end effect
                mfield.GetComponent <MAgneticField>().on = false;
                GameMaster.instance.UI.toggle(3, false);
                on = false;
            } 

        }
    }
    public void StartMagnet()
    {
        on = true;
        timer = GameMaster.instance.LC.levelTime+ duration;
        mfield.GetComponent<MAgneticField>().on = true;
        GameMaster.instance.UI.toggle(3,true);
        //turn on magnetic field
    }
}
