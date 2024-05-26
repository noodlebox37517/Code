using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour {
    public bool Fade = false;
    private float opacity = 1f;
    public float final_opacity = .5f;
    public bool Move = false;
    [Tooltip("Must add upto 1 and not more")]
    public Vector2 moveDirection = new Vector2(0f,0f);
    public float moveSpeed = 0.1f;
    public float Lingertime = 1f;
    private float starttime;

    // Use this for initialization
    void Start () {
        starttime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Master.instance.pause)
        {
            if (Time.time >= starttime + Lingertime)
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (Fade)
                {
                    opacity = (((starttime + Lingertime) - Time.time) * (1f - final_opacity)) + final_opacity;
                    Color temp = this.GetComponent<Text>().color;
                    temp.a = opacity;
                    this.GetComponent<Text>().color = temp;
                    //fade
                }
                if (Move)
                {
                    transform.position = transform.position + (Vector3)(moveDirection * (moveSpeed * Time.deltaTime));
                }

            }
        }
    }
}
