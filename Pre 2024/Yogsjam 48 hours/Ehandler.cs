using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ehandler : MonoBehaviour
{
    public GameObject etext;
    public Vector3 offset = new Vector3(0f, 1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Interactables>().Closestsinteractable != null && Game.instance.santa.carrying == null)
        {
            etext.SetActive(true);
            etext.transform.position =  this.GetComponent<Interactables>().Closestsinteractable.transform.position+ offset;
             //transform.  (Game.instance.cam.gameObject.transform.rotation);
            Debug.Log("why");
             etext.transform.LookAt(Game.instance.cam.gameObject.transform.position);
            etext.transform.Rotate(0f, 180, 0f);

        }
        else
        {
            etext.SetActive(false);
        }
    }
}
