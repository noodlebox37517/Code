using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashingtext : MonoBehaviour
{
    /// <summary>
    /// time to cycle between 0 and 100 opacity
    /// </summary>
    public float cycleTime = 1f;
    private float starttime=0f;
    private float pos = 1;
    // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= starttime + cycleTime)
        {
            starttime = Time.time;
        }
        else
        {
            float cyclepos =   (Time.time - starttime)/ cycleTime;
            Color col = this.GetComponent<Text>().color;
            col.a = cyclepos;
            pos = cyclepos;
            this.GetComponent<Text>().color = col;

        }
    }
}
