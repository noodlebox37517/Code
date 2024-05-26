using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour {
    public bool Lastlink = false;
    public Transform firstlinkedChain;
    public Transform linkedChain;
    public Vector3 linkedPos = new Vector3(0f, 0f, 0f);
    public float spriteOffset;
    public float linkedSpriteOffset;
    public int chainNO = 1;
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (firstlinkedChain != null )
        {
           // Lookat(firstlinkedChain.position);
            transform.position = firstlinkedChain.position + linkedPos;
            
        }

    }
    public void Lookat(Vector3 pos)
    {
        Vector3 direction = (pos - transform.position).normalized;
       float tarAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = transform.rotation = Quaternion.AngleAxis(tarAngle, Vector3.forward);
        linkedPos = direction * (spriteOffset + linkedSpriteOffset)*(chainNO);
        linkedPos *= -1;
    }

    public void Setup(Transform linked, Transform lastlink , int num, bool last)
    {
        Lastlink = last;
        //if (linked != lastlink)
        firstlinkedChain = lastlink;
        linkedChain = linked;
        chainNO = num;

        spriteOffset = transform.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
        linkedSpriteOffset = linkedChain.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
        Lookat(firstlinkedChain.position);
    }
}
//currentendpos = currentendpos - (currentendpos - pointB).normalized* Objectsinwall[g].gameObject.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2f;