using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interatable.Inventory;
using UnityEngine;

public class LockedInteractable : Interactable, IBlockable
{

    private Action KeyFound;
    [SerializeField] private GameObject _keyGameObject;
    private List<GameObject> _blockers = new();
    private IStorable storable;

    private bool _isLocked;
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        if (_keyGameObject == null) 
            return; 
        storable = _keyGameObject.GetComponent<IStorable>();
        if (storable != null)
        {
            storable.PickedUp += Unlock;
            Lock();
        }
    }
    
    public void Unlock()
    {
        _isLocked = false;
        if (storable == null)
            return;
        
        storable.PickedUp -= Unlock;
        CheckInteractable();
    }

    private void Lock()
    {
        _isLocked = true;
        CheckInteractable();
    }

    public void AddBlocker(GameObject go)
    {
        _blockers.Add(go);
        CheckInteractable();
    }
    
    public void RemoveBlocker(GameObject go)
    {
        _blockers.Remove(go);
        CheckInteractable();
    }

    public void CheckInteractable()
    {
        if (_blockers.Count == 0 && _isLocked == false)
        {
            isInteractble = true;
            return;
        }
        isInteractble = false;
    }
}
