using System.Collections;
using System.Collections.Generic;
using Code.UI;
using UnityEngine;

public class Padlock : Interactable
{
    [SerializeField] private List<GameObject> _blockedBlockables;
    [SerializeField] private GameObject _padlockUIGO;
    
    private void OnEnable()
    {
        foreach (var blockable in _blockedBlockables)
        {
            blockable.GetComponent<IBlockable>().AddBlocker(gameObject);
        }
        //change to code mini game
        Interacted += StartPadLockPuzzle;
        singleUse = false;
    }

    protected override void OnDisable()
    {
        UnBlock();
        base.OnDisable();
    }
    public void UnBlock()
    {
        foreach (var blockable in _blockedBlockables)
        {
            blockable.GetComponent<IBlockable>()?.RemoveBlocker(gameObject);
        }
        Interacted -= StartPadLockPuzzle;
    }

    private void StartPadLockPuzzle()
    {
        isInteractble = false;
        GameManager.MainPlayer.InteractableDisabled(this);
        _padlockUIGO.GetComponent<PadlockUI>().Active(true);
    }

    private void StopPadLockPuzzle()
    {
        _padlockUIGO.GetComponent<PadlockUI>().Active(false);
        isInteractble = true;
    }

    public override void TriggerLeft()
    {
        StopPadLockPuzzle();
        base.TriggerLeft();
    }
}
