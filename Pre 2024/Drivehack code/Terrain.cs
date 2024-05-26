using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public bool autoLength = true;
    public float Length =1000f;
    // Start is called before the first frame update
    void Start()
    {
        if(autoLength)
        Length = GetComponent<Renderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
