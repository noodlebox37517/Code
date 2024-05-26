using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponentInParent<Cookietable>().cookiegos.Add(this.gameObject);
        transform.GetComponentInParent<Cookietable>().primetable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
