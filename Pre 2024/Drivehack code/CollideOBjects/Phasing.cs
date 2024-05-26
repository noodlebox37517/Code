using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phasing : MonoBehaviour
{
    public float cyclespeed = .5f;
    public float cyclepos = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cyclepos += cyclespeed * Time.deltaTime;
        if (cyclepos >= 1)
        {
            cyclepos = 0;
            Hide();
        }
    }
    public void Hide()
    {
        this.GetComponent<MeshRenderer>().enabled = !this.GetComponent<MeshRenderer>().enabled;
    }
}
