using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILvlUpgrade : UIBaseUpgrade {


    public Slider lvlbar;
    public int lvl;
    public int startlvl;
    public float refund  = 0.5f;


    public Text UpCost ;
    public Text DownCost;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PosRequest()
    {
        if (lvl >= 5)
        {
            Debug.Log("max level");
        }
        else
        {
            
            //ask if there is enough money to support cost
            if (UIMasterScript.FreeCash() - (UIMasterScript.passiveBaseCostList[gradeID] * (lvl + 1)* Discount) >= 0)
            {
                UIMasterScript.currentCosts += (int)(UIMasterScript.passiveBaseCostList[gradeID] * (lvl + 1) * Discount);
                lvl++;
                UIMasterScript.passiveList[gradeID] = lvl;

            }
            else
            {
                Debug.Log("Insufficient funds");
            }
            //if yes increase lvl
            UpdateGraphic();
        }
    }
    public void NegRequest()
    {
        if (lvl > 0)
        {

            float mod =1f;
            if (lvl == startlvl)
            {
                mod = refund;
            }
            else
            {
                mod = Discount;
            }
            UIMasterScript.currentCosts -= (int) ((UIMasterScript.passiveBaseCostList[gradeID] * lvl)* mod );
            lvl--;
            UIMasterScript.passiveList[gradeID] = lvl;
        }
        UpdateGraphic();
    }
    public void UpdateGraphic()
    {
        float mod = 1f;
        if (lvl == startlvl)
        {
            mod = refund;
        }
        else
        {
            mod = Discount;
        }
        lvlbar.value = lvl;
        if (lvl != 5)
            UpCost.text = ((int)(UIMasterScript.passiveBaseCostList[gradeID] * (lvl + 1) * Discount)).ToString();
        else
            UpCost.text = "Max";
        if (lvl > 0)
            DownCost.text = ((int)((UIMasterScript.passiveBaseCostList[gradeID] * lvl) * mod)).ToString();
        else
            DownCost.text = 0.ToString();

       //update costs
       UIMasterScript.UpdateGraphic();
    }
    public override void Initilize(UpgradeMenu menu, int id)
    {
        base.Initilize(menu,  id);
        // get level
        lvl = UIMasterScript.passiveList[id];
        startlvl = lvl;
        UpdateGraphic();
    }
}
