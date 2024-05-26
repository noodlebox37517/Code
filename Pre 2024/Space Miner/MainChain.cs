using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChain : MonoBehaviour {
    public GameObject linkPrefab;
    public GameObject AtachObject;
    public List<GameObject> midLinks;
    public GameObject endLink;
    public float linkLength = 1f;
    public Vector3 elPos;
    public bool live =false;
    public int maxLinks =100;

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (live)
        {
           
            while ((this.transform.position - midLinks[midLinks.Count - 1].transform.position).magnitude / linkLength > 1f  && midLinks.Count < maxLinks && live)
            {
                
                if (Vector3.Dot((transform.position - midLinks[0].transform.position).normalized, (transform.position - midLinks[midLinks.Count - 1].transform.position).normalized) > 0f) {
                    // Debug.Log((this.transform.position - midLinks[midLinks.Count - 1].transform.position).magnitude / linkLength + " " + midLinks[midLinks.Count - 1].transform.position + " " + (midLinks.Count - 1) + " " + midLinks[midLinks.Count - 1].name);
                    //Debug.Log(Vector3.Dot((transform.position - midLinks[0].transform.position).normalized, (transform.position - midLinks[midLinks.Count - 1].transform.position).normalized));
                    GameObject templink = Instantiate<GameObject>(linkPrefab, transform);
                    Vector3 direction = (transform.position - midLinks[0].transform.position).normalized;
                    float tarAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    float spriteOffset = templink.transform.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
                    float linkedSpriteOffset = midLinks[0].transform.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2f;
                    templink.transform.position = midLinks[0].transform.position + direction * (spriteOffset + linkedSpriteOffset) * (midLinks.Count);
                    templink.GetComponent<ChainLink>().Setup(midLinks[midLinks.Count - 1].transform, midLinks[0].transform, midLinks.Count, false);
                    midLinks.Add(templink);
                    templink.name = "chain clone no " + midLinks.Count;
                }

                else {
                    if (midLinks[midLinks.Count - 1] != null) {
                        Destroy(midLinks[midLinks.Count - 1]);
                        midLinks.RemoveAt(midLinks.Count - 1);
                    }
                }

            }
            //while distance from lastmade link to this.transform.position / length of link > 1
            //add link
        }
    }
  

    public void Setup(GameObject attachto)
    {
        AtachObject = attachto;
        GameObject templink = Instantiate<GameObject>(linkPrefab,transform); //spawn attach
        endLink = templink;
        Vector3 direction = (transform.position - AtachObject.transform.position).normalized;
        float tarAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        float spriteOffset = templink.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        float linkedSpriteOffset = AtachObject.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        templink.transform.position = AtachObject.transform.position + direction * (spriteOffset + linkedSpriteOffset);
        templink.GetComponent<ChainLink>().Setup(AtachObject.transform, AtachObject.transform, 1 , true);
        midLinks.Add(templink);
        templink.name = "first chain clone no " + midLinks.Count;
        live = true;
        linkLength = spriteOffset * 2;
        elPos = endLink.transform.position;

    }
    public void DestoryChain()
    {
        live = false;
        //foreach (GameObject g in midLinks)
        //{
        //    Destroy(g);
        //    midLinks.Remove(g);
        //}
        Destroy(this.gameObject);
    }
}
