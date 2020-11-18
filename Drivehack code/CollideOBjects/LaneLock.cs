using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneLock : MonoBehaviour
{
    public float revealDistance = 5f;
    public bool locked = false;
    public GameObject Lanelock;
    // Start is called before the first frame update
    void Start()
    {
        //Hide();
    }

    // Update is called once per frame
    void Update()
    {
        // dragon is at 0 //TODO lock to actual pos
        float dist = this.transform.position.z;
        if (!locked&& dist <= revealDistance)
        {
            //Hide();
            Instantiate<GameObject>(Lanelock,this.transform.position+ new Vector3(0f,.75f,0f),Quaternion.identity);
            locked = true;
        }
    }
   
}
