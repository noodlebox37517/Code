using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayed : MonoBehaviour
{

    //hide on spawn, rveal when distance to player is lessor= to revealdist
    /// <summary>
    /// distance from player object will be revealed
    /// </summary>
    public float revealDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        // dragon is at 0
        float dist = this.transform.position.z;
        if (!this.GetComponent<MeshRenderer>().enabled && dist <= revealDistance)
        {
            Hide();
        }
    }
    public void Hide()
    {
        this.GetComponent<MeshRenderer>().enabled = !this.GetComponent<MeshRenderer>().enabled;
    }
}
