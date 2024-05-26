using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadechecker : MonoBehaviour
{
    public bool clear =true;
    public List<Collider> inside;

    private void Update()
    {
        if (inside.Count == 0)
        {
            clear = true;
        }
        else
        {
            clear = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        inside.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        inside.Remove(other);
    }
}
