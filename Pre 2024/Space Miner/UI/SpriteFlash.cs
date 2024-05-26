using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFlash : MonoBehaviour {

    public float freq = 0.2f;
    private float lastswap;
    private bool flash = false;
    public Color bright;
    public Color normal;
	// Use this for initialization
	void Start () {
        normal = this.GetComponent<Image>().color;
        lastswap = Time.realtimeSinceStartup;
    }
	
	// Update is called once per frame
	void Update () {
		if(lastswap+freq < Time.realtimeSinceStartup)
        {
            lastswap = Time.realtimeSinceStartup;
            if (flash)
            {
                this.GetComponent<Image>().color = bright;
            }
            else
            {
                this.GetComponent<Image>().color = normal;
            }
            flash = !flash;
        }
	}
}
