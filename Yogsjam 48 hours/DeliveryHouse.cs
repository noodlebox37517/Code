using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryHouse : MonoBehaviour
{
    public int score;
    public string target = "Player";
    public string presenttag = "Gift";
    public Transform droppoint;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == target)
        {
            //check if holding present
            if(other.GetComponent<Santa>().carrying != null)
            {
                if (other.GetComponent<Santa>().carrying.tag == presenttag)
                {

                    GameObject prese = other.GetComponent<Santa>().AcceptGift();
                    if (droppoint != null)
                    { 
                        prese.transform.position = droppoint.position;
                        prese.GetComponent<Present>().Remove();
                        prese.transform.SetParent(transform);
                    }
                else
                {
                    prese.GetComponent<Present>().Remove();
                    Destroy(prese);
                }
                    Delivered();

                }
            }
        }
    }
    public void Delivered()
    {
        Game.instance.delcomplete();
        Destroy(this.gameObject);
    }
}
