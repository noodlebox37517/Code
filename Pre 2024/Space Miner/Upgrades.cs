using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public int maxLevel = 5;
    public int aimSpeedLVL = 0;
    public int fireSpeedLVL= 0;

    public int pullPowerLVL = 0;

    public float speedmod = 1f;

    public bool Hooksize = false;
    public bool scannerUpgrade = false;
    public bool aimUpgrade = false;
    public bool BackGrab = false;
    public bool Destroyer = false;
    public bool autograb = false;

    private Resources sources;
    
    // Use this for initialization
    void Start () {
        sources = Master.instance.GetComponent<Resources>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyUpgrades()
    {

    }

    public void Upgrade(string upgradename, int lvl)
    {
        switch (upgradename)
        {
            case "AimSpeed":
                aimSpeedLVL = lvl;
                break;
            case "FireSpeed":
                fireSpeedLVL = lvl;
                break;
            case "PullPower":
                pullPowerLVL = lvl;
                break;

            case "Hook":
                Hooksize = true;
                break;
            case "Scanner":
                scannerUpgrade = true;
                break;
            case "Aim":
                aimUpgrade = true;
                break;

            default:
                Debug.Log("upgrade not recognized " + upgradename);

                break;
        }
    }
    /// <summary>
    /// sets game to bought actives
    /// </summary>
    public void LoadActives()
    {
        BackGrabToggle(BackGrab);
        TarAssistToggle(aimUpgrade);
        Destroyertoggle(Destroyer);
        AutoCollectToggle(autograb);
        BigHookToggle(Hooksize);
        //reset grabber visuals
        ResetGrabberPointer();

    }

    public void ResetGrabberPointer()
    {
        int spritepos = 0;
        if(BackGrab|| Hooksize)
        {
            if (BackGrab && Hooksize)
            {
                spritepos = 3;
            }
            else if (!BackGrab)
            {
                spritepos = 2;
            }
            else
            {
                spritepos = 1;
            }
        }
        Master.instance.Targeterobj.GetComponent<Targeter>().tarpoint.GetComponent<TargetPointer>().PointerSprite(spritepos);


    }
    /// <summary>
    /// unloads all actives/resets game
    /// </summary>
    public void UnloadActives()
    {
        BackGrabToggle(false);
        TarAssistToggle(false);
        Destroyertoggle(false);
        AutoCollectToggle(false);
        BigHookToggle(false);
    }
    private void AutoCollectToggle(bool active)
    {
        if (active)
        {
            Master.instance.Targeterobj.GetComponent<Targeter>().autocollector.SetActive(true);
            Master.instance.Targeterobj.GetComponent<Targeter>().autocollector.GetComponent<autocollector>().active = true;
        }
        else
        {
            Master.instance.Targeterobj.GetComponent<Targeter>().autocollector.GetComponent<autocollector>().active = false;
            Master.instance.Targeterobj.GetComponent<Targeter>().autocollector.SetActive(false);
            
        }
    }

    private void ScannerToggle(bool active)
    {
        if (active)
        {

        }
        else
        {

        }
    }
    private void BigHookToggle(bool active)
    {
        if (active)
        {
            if (BackGrab)
            {
                sources.Hook = sources.hooks[3];
            }
            else
            {
                sources.Hook = sources.hooks[2];
            }
        }
        else
        {
            sources.Hook = sources.hooks[0];
        }
    }
    private void BackGrabToggle(bool active)
    {
        if (active)
        {
            //set return grab collider to false (as in keep active)
            //swap hooktemp for backgrab 
            sources.Hook = sources.hooks[1];
            Master.instance.Targeterobj.GetComponent<Targeter>().grabOnRetract = true;
        }
        else
        {
            sources.Hook = sources.hooks[0];
            Master.instance.Targeterobj.GetComponent<Targeter>().grabOnRetract = false;
        }
    }

    private void Destroyertoggle(bool active)
    {
        if (active)
        {
            Master.instance.destoryOnTouch = true;
        }
        else
        {
            Master.instance.destoryOnTouch = false;
        }

    }
    private void TarAssistToggle(bool active)
    {
        if (active)
        {
            //turn line on
            //set travel speed modifier to .5
            speedmod = .5f;
            //targetr
            Master.instance.Targeterobj.GetComponent<Targeter>().drawTarLine = true;
            //linerender
            Master.instance.Targeterobj.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            //turn line off
            //set travel speed modifier to 1
            speedmod = 1f;
            Master.instance.Targeterobj.GetComponent<Targeter>().drawTarLine = false;
            //linerender
            Master.instance.Targeterobj.GetComponent<LineRenderer>().enabled = false;
        }
    }
    public void SetSavedGrades(int[] passives, bool[] actives)
    {
        aimSpeedLVL = passives[0];
        fireSpeedLVL = passives[1];
        pullPowerLVL = passives[2];

        Hooksize = actives[0];
        scannerUpgrade = actives[1];
        aimUpgrade = actives[2];
        BackGrab = actives[3];
        Destroyer = actives[4];
        autograb = actives[5];


}
}
