using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{

    public virtual bool Place()
    {
        Debug.Log("attempt place");
        return false;
    }
}
