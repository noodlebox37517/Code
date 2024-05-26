using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Effects : MonoBehaviour
{

    public bool blinded = false;
    public float blindtimer = 5f;
    public float blindtimerstart = 0f;

    public bool Slow = false;
    public float slowtimer = 5f;

    private void Update()
    {
        if (blinded)
        {
            if(blindtimerstart+blindtimer < GameMaster.instance.LC.levelTime)
            {
               
                UnBlind();
            }
            //checked if turn off
        }
        if (Slow)
        {
            if (GameMaster.instance.LC.levelTime >= slowtimer)
            {
                
                StopSlow();
            }
        }

    }

    public void Blind()
    {
        if (!blinded)
        {
            GameMaster.instance.UI.toggle(0, true);
            blinded = true;
            GameMaster.instance.CamC.Toggle("LevelElement");
            blindtimerstart = GameMaster.instance.LC.levelTime;
            //GameMaster.instance.UI.blindsprite.SetActive(true);
            //Debug.Log("blinded");
        }
    }
    public void UnBlind()
    {
        blinded = false;
        GameMaster.instance.CamC.Toggle("LevelElement");
        GameMaster.instance.UI.toggle(0, false);
        //GameMaster.instance.UI.blindsprite.SetActive(false);
        Debug.Log("unblinded " + blindtimer);
    }

    public void StartSlow(float duration)
    {
        GameMaster.instance.UI.toggle(1, true);
        slowtimer = GameMaster.instance.LC.levelTime + duration;
        GameMaster.instance.GetComponent<InputController>().speedmod = .3f;
        Slow = true;
    }
    public void StopSlow()
    {
        GameMaster.instance.UI.toggle(1, false);
        GameMaster.instance.GetComponent<InputController>().speedmod = 1f;
        Slow = false;
        Debug.Log("unslowed " + slowtimer);
    }
}
