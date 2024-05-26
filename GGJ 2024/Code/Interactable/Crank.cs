using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crank : Interactable
{
    private  Iinteractable _interactable;
    [SerializeField] private bool _isInteractOnEnd = false;
    [SerializeField] private Transform _PulledGO;
    [SerializeField] private Transform _closeLinePoint;
   
    [Range(0,1)][SerializeField] private float _crankedAmount;
    [SerializeField] private float _timeToFullCrank = 5f;

    private bool _isCranking = false;
    private float _crankStartTime;
    private Vector3 _startpos;
    private Vector3 _finalpos;
    
    private void OnEnable()
    {
        Interacted += StartCrank;
    }
    
    private void StartCrank()
    {
        _crankStartTime = Time.time;
        _startpos = _PulledGO.transform.position;
        var diff = _closeLinePoint.position - _startpos ;
        _finalpos = _startpos + (diff).normalized * (diff.magnitude * _crankedAmount);
        _isCranking = true;
        
    }

    protected override void Start()
    {
        _interactable = _PulledGO?.GetComponent<Iinteractable>();
        if (_interactable != null)
        {
            _interactable.isInteractble = false;
        }
        base.Start();
    }


    private void Update()
    {
        if (_isCranking == false) 
            return;
        
        if (Time.time > _crankStartTime+ _timeToFullCrank)
        {
            _PulledGO.position = _finalpos;
            _isCranking = false;
            if (!_isInteractOnEnd)
                return;
            
            if (_interactable != null)
            {
                _interactable.isInteractble = true;
                _interactable.Interact();
            }
        }
        else
        {
            var t = (Time.time - _crankStartTime) * ( 1 / _timeToFullCrank);
            _PulledGO.position = Vector3.Lerp(_startpos, _finalpos, t );
        }
    }
}
