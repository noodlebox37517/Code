using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseUpgrade : MonoBehaviour {
    public UpgradeMenu UIMasterScript;
    public int gradeID = 0;
    public float Discount = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Initilize(UpgradeMenu menu, int id)
    {
        UIMasterScript = menu;
        gradeID = id;
        Discount = 1f;
    }
}
