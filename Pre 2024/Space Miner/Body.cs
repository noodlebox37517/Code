using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    private Rigidbody2D RB;
    public float startSpeed =0.1f;
	// Use this for initialization
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
	public virtual void   Start () {
        //RB.velocity = Vector2.down* startSpeed;
	}

    public virtual void RemoveSelf()
    {
        Destroy(this.gameObject);
    }
    public virtual void Shatter()
    {
        RemoveSelf();
    }

    // Update is called once per frame
    void Update () {
		if(Time.timeScale > 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>().isVisible)
            {

            }
        }
	}
    void OnBecameInvisible()
    {
       // Debug.Log("tadah!");
        RemoveSelf();
    }
    public void Launch(Vector3 tardirection,float sped)
    {
        RB.velocity = tardirection * sped;
        float tarAngle = (Mathf.Atan2(tardirection.y, tardirection.x) * Mathf.Rad2Deg);
        transform.rotation = transform.rotation = Quaternion.AngleAxis(tarAngle, Vector3.forward);
    }
}
