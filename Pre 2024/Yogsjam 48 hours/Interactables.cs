using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Transform interactor;
    public float maxinteractdist = 3f;
    public List<GameObject> interactables;

    public GameObject Closestsinteractable;


    private void Start()
    {
        interactor = Game.instance.santa.gameObject.transform;
    }
    private void Update()
    {
        Closestsinteractable = null;
        if (interactables.Count > 0)
        {
            float dist = maxinteractdist;
            
            foreach (GameObject  g in interactables)
            {
                if (g != null)
                {
                    if ((interactor.position - g.transform.position).magnitude <= dist)
                    {
                        dist = (interactor.position - g.transform.position).magnitude;
                        Closestsinteractable = g;
                    }
                }
                //check distance
            }
        }
    }
    public bool Interact()
    {
        if(Closestsinteractable != null)
        {
            Closestsinteractable.GetComponent<Interactable>().Interact();
            Closestsinteractable = null;
            return true;
           

        }
        return false;
    }
}
