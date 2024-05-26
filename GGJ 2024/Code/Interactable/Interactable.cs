using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    public bool isInteractble {get; set;}

    public void Interact();
    public GameObject go { get; }

    public void TriggerLeft();
    public Action Interacted { get; set; }
}

public class Interactable : MonoBehaviour, Iinteractable
{
    public bool isInteractble
    {
        get
        {
            if (_isUsable == false)
            {
               return false; 
            }

            return _isInteractable;
        }
        set
        {
          _isInteractable = value;
        }
    }

    
    private bool _isInteractable = true;
    private bool _isUsable = true;
    [SerializeField] protected bool singleUse = true;
    [SerializeField] private AudioClip _ClipOnInteract;
    public Action Interacted { get; set; }
    public virtual void Interact()
    {
        if(isInteractble == false)
            return;

        if (_ClipOnInteract != null)
        {
            AudioManager.instance.PlayClip(_ClipOnInteract, transform.position);
        }
        Interacted?.Invoke();
        if (singleUse)
        {
            _isUsable = false;
            //remove from players list of interactables
            GameManager.MainPlayer.InteractableDisabled(this);
        }
        Debug.Log($"you have interacted with {gameObject.name}");
    }

    public GameObject go => gameObject;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        var cols = GetComponents<Collider>();
        Collider triggerCol= null;
        foreach (var col in cols)
        {
            if (col.isTrigger)
            {
                triggerCol = col;
            }
        }

        if (triggerCol == null)
        {
            var col =  gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = GameManager.instance.gameData.DefualtInteractableTriggerRadius;
        }
    }

    protected virtual void OnDisable()
    {
        GameManager.MainPlayer.InteractableDisabled(this);
    }

    public virtual void TriggerLeft()
    {
        
    }
}
