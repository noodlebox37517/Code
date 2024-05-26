using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public interface IIlusionAble
{
    public bool ToggleIllusion(bool isNowActive);

    public bool SetAsSlave(IIlusionAble master);

}
public class IllusionObject : MonoBehaviour, IIlusionAble
{
    
    [SerializeField] private bool[] isActiveInState = new bool[IllusionManager.TotalLevelStates];
    
    [Header("Slave object settings")]
    [SerializeField] private List<GameObject> slaveObjects;

    [Header("Slave Only")] [SerializeField]
    private bool isCopyingMaster = true;
   
    private bool _isSlave;
    private bool _isActive;
   
    //todo Add slave logic 
    // Start is called before the first frame update

    private void Awake()
    {
        IllusionManager.StateChanged += StateChanged;
        //Debug.Log($"delegate count{ IllusionManager.StateChanged.GetInvocationList().Length}");
        //worried for nothing ?;
        for (var index = 0; index < slaveObjects.Count; index++)
        {
            var slave = slaveObjects[index];
            if (slave.GetComponent<IIlusionAble>().SetAsSlave(this) == false)
            {
                slaveObjects.Remove(slave);
            }
        }
    }

    private void OnDestroy()
    {
        IllusionManager.StateChanged -= StateChanged;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateChanged(int newState)
    {
        if (_isSlave == false)
        {
            if (newState >= isActiveInState.Length)
            {
                return;
            }
                
            ToggleIllusion(isActiveInState[newState]);
            foreach (var slave in slaveObjects)
            {
                slave.GetComponent<IIlusionAble>().ToggleIllusion(_isActive);
            }
        }
        
    }

    private void Hide(bool isHiding)
    {
        gameObject.SetActive(!isHiding);
    }
    public  bool ToggleIllusion(bool isNowActive)
    {
        if (_isSlave && isCopyingMaster == false)
        {
            _isActive = !isNowActive;
        }
        else
        {
            _isActive = isNowActive;
        }
        Hide(!_isActive);
        return _isActive;
    }

    public bool SetAsSlave(IIlusionAble master)
    {
        if (slaveObjects.Count > 0)
        {
            Debug.LogWarning($"Illusionable found to have slaves, cannot nest slavery {gameObject.name}");
            return false;
            
        }
        _isSlave = true;
        return _isSlave;
    }
}
