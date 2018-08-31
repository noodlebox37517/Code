using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUpgradeToggleListener : UIBaseUpgrade {

    Toggle m_Toggle;
    public bool ignorenextchange = false;
    public int index;


    // Use this for initialization
    void Start () {
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, and output the state
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        //Initialize the Text to say whether the Toggle is in a positive or negative state
        

    }

    //Output the new state of the Toggle into Text when the user uses the Toggle

    public void ToggleValueChanged(Toggle change)
    {
        if (!ignorenextchange)
        {
            if (change.isOn)
            {
                //check cash
                if (UIMasterScript.FreeCash() - UIMasterScript.activeBAseCostList[gradeID] >= 0)
                {
                    UIMasterScript.currentCosts += (int)(UIMasterScript.activeBAseCostList[gradeID] * Discount);
                    UIMasterScript.activeList[index] = true;
                    UIMasterScript.UpdateGraphic();
                }
                else
                {
                    Debug.Log("Insufficient funds");
                    ignorenextchange = true;
                    m_Toggle.isOn = !m_Toggle.isOn;
                }
            }
            else
            {
                //remove cash/ 
                //unless already baught then change back to true
                if (UIMasterScript.activeList[gradeID])
                {
                    ignorenextchange = true;
                    m_Toggle.isOn = !m_Toggle.isOn;

                }
                else
                {
                    UIMasterScript.currentCosts -= (int)(UIMasterScript.activeBAseCostList[gradeID] * Discount);
                    UIMasterScript.activeList[index] = false;
                    UIMasterScript.UpdateGraphic();
                }
            }
        }
        else
        {
            ignorenextchange = false;
        }

    }

    //listen if(true) else if(false) remove cost
    //check if cost will go over cash
    //apply cost


    // Update is called once per frame
    void Update () {
		
	}
    public void LoadCashCost()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Text>())
            {
                transform.GetChild(i).GetComponent<Text>().text = ((int)(UIMasterScript.activeBAseCostList[gradeID]*Discount)).ToString();
            }
        }
    }
}
