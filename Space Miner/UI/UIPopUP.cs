using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUP : MonoBehaviour {
    public Text popText;
    public bool puaseOnUp = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Open(string txt)
    {
        if (puaseOnUp)
            Master.instance.PauseLVL(true);
        popText.text = txt;

        //set intercept in control
    }
    public void Close()
    {
        if (puaseOnUp)
        {
            if(UI_Control.instance.currentUI == UI_Control.ActiveUI.LvlPanel)
             Master.instance.PauseLVL(false);
        }
        Destroy(gameObject);

        //unset intercept in control
    }
}
