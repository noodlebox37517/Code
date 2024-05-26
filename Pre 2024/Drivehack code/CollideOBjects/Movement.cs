using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool magnetized = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.LC.LevelPlay)
        {
            if (!magnetized)
                transform.position += new Vector3(0, 0, -GameMaster.instance.LC.speedMod * Time.deltaTime);
            else
            {
                float speed = 8f;
                //transform.position += new Vector3(0, 0, -GameMaster.instance.LC.speedMod * Time.deltaTime);
                float attraction = ( GameMaster.instance.dragon.transform.position - this.transform.position).sqrMagnitude;
                attraction = Mathf.Min(attraction, speed);
                //transform.position -= (this.transform.position - GameMaster.instance.dragon.transform.position).normalized * (speed - attraction) * Time.deltaTime;
                transform.position -= (this.transform.position - GameMaster.instance.dragon.transform.position).normalized * (GameMaster.instance.LC.speedMod + speed - attraction) * Time.deltaTime;
            }
        }
    }
}
