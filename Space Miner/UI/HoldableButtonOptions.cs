using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableButtonOptions : MonoBehaviour
{

    // Use this for initialization
    public bool right = true;
    void Start()
    {
        gameObject.GetComponent<HoldableButton>().right = right;

    }
}
