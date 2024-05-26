using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyself : MonoBehaviour
{
    public float destroyAfter =1f;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timer+ destroyAfter)
        {
            Destroy(this.gameObject);
        }
    }
}
