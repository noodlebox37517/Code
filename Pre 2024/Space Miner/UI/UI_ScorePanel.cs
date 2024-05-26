using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScorePanel : MonoBehaviour {

    public Text lvlTitle;
    public GameObject roidCounterPanel;
    public Text cashMadeCounter;
    public GameObject ResultPanel;

    public bool count = false;
    public bool Skip = false;
    public List<GameObject> collectedOres;

    public GameObject imgfab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        //while loop for roids

        //while loop for cash 
            //or do both at once
	}

    public void Setup()
    {
        Map tempmap = Master.instance.LoadedLevel.GetComponent<Map>();
        lvlTitle.text = tempmap.GetComponent<Level>().lvlName;

        
        cashMadeCounter.text = Master.instance.levelMoney.ToString();

        //start roid counter
        List<RoidCounter.roidvalue> roids = tempmap.GetComponent<RoidCounter>().collectedobjs;

        foreach (Transform child in roidCounterPanel.transform)
        {
            Destroy(child.gameObject);
        }

            foreach  (RoidCounter.roidvalue r in roids)
        {
          GameObject tempgo = Instantiate<GameObject>(imgfab, roidCounterPanel.transform);
            tempgo.GetComponent<Image>().sprite = r.roidSprite;
        }
    }
}
