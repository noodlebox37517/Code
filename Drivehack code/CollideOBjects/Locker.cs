using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    private float timer = 0f;
    public float hangTime = 1f;
    public float droptime = .5f;
    public float stayTime = 1f;
    public float targethieght = 0;
    private float dropstep = .1f;
    // Start is called before the first frame update
    void Start()
    {
        dropstep = (  this.transform.position.y - targethieght) / droptime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.LC.LevelPlay)
        {
            timer += Time.deltaTime;

            if (timer > hangTime)
            {
                if (timer< hangTime + droptime )
                {
                    transform.position += (-transform.up * (dropstep * Time.deltaTime));
                }
                else if(timer > hangTime + droptime + stayTime)
                {
                    Destroy(this.gameObject);
                }
            }
            //move locker
        }
    }
}
