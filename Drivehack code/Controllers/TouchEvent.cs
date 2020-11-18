using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    public int CommandID = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public virtual void Touched()
    {
        switch (CommandID)
        {
            case 0:
                //start level
                GameMaster.instance.LC.StartLevel(0);
                GameMaster.instance.UI.SwitchPanels(0);
            break;
            case 1:
                //pause/unpuase
                GameMaster.instance.LC.PuaseLevel(false);
                GameMaster.instance.UI.SwitchPanels(2);
                break;
            case 2:
                //pause/unpuase
                GameMaster.instance.LC.PuaseLevel(true);
                GameMaster.instance.UI.SwitchPanels(0);
                break;
            case 3:
                GameMaster.instance.UI.SwitchPanels(1);
                GameMaster.instance.UI.SaveUpgrades();
                break;
            case 4:
                GameMaster.instance.UI.SwitchPanels(5);
                GameMaster.instance.UI.LoadUpgrades();
                break;
        }
        
    }
}
