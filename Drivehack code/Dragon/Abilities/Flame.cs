using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float Time = 5f;
    public Transform anchor;
    public float floatAnchorOffseset = 0.5f;
    private float StartTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        anchor = GameMaster.instance.dragon.anchor;
        StartTime = GameMaster.instance.LC.levelTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.instance.LC.levelTime>= StartTime+Time)
        {
            EndFlame();
        }
        this.transform.position = anchor.position + new Vector3(0f,0f,floatAnchorOffseset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollidableObject>()!= null)
        {
         if(other.GetComponent<CollidableObject>().Data.Contact(1))
            {
                Destroy(other.gameObject);
                Debug.Log("burned");
            }
         else if (other.GetComponent<CollidableObject>().Data.Contact(2))
            {
                other.GetComponent<FoodData>().cooked = true;
                Debug.Log("cooked");
            }
        }
        //call cooked on object
    }

    public void EndFlame()
    {
        Destroy(this.gameObject);
    }
}
