using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAgneticField : MonoBehaviour
{
    public bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (on)
        {
            if (other.GetComponent<FoodData>() != null)
            {
                other.GetComponent<Movement>().magnetized = true;
            }
        }
    }
}
