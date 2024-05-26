using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAvpointer : MonoBehaviour
{
    public Transform from;
    public Transform to;
    public float dist = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(to!= null)
        {
            this.GetComponent<MeshRenderer>().enabled = true ;
            Vector3 dir = (to.position - from.position).normalized;
            transform.rotation.SetLookRotation(dir);
            transform.position = new Vector3( from.position.x, .5f, from.position.z) + (dir * dist);
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
