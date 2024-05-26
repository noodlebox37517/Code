using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartPanel : MonoBehaviour {

    public Text lvlTitle;
    public Text minCashReq;
    public Transform hoversParent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLevelData(Level lvlmap)
    {
        //title 
        lvlTitle.text = lvlmap.lvlName;
        //cash
        minCashReq.text = lvlmap.cashTarget.ToString();
        //object_ icons
        foreach (Transform child in hoversParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<GameObject> temphovers = lvlmap.hoverObjects;
        int numhovers = temphovers.Count;
        // calculate grid
        for (int i =0; i < numhovers; i++)
        {
            GameObject temphover = Instantiate<GameObject>(temphovers[i],hoversParent); 
        }
    }
}
