using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPanelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void close()
    {
       this.gameObject.SetActive(false);
    }
}
