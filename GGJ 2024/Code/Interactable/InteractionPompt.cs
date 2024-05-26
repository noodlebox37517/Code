using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPompt : MonoBehaviour
{
    private const float smoothTime =.2f;
    private Vector3 velocity = Vector3.zero;
    public static InteractionPompt instance;
    public const float  promptPosRatio = .75f;
    public GameObject targetInteractable;
    
    private Transform _playerTransform;
    private SpriteRenderer _spriteRenderer;
    
    private void OnEnable()
    {
        instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetPlayer();
    }

    private void Update()
    {
        if (targetInteractable != null && targetInteractable.activeSelf)
        {
            var diff = _playerTransform.position - targetInteractable.transform.position;
            var pos = targetInteractable.transform.position + (diff).normalized * (diff.magnitude * promptPosRatio) ;
                //transform.position = pos;
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
            transform.LookAt(_playerTransform);
        }
        else if(_spriteRenderer.enabled)
        {
            //catch if object is no longer available
            DeActivePrompt();
        }
    }
    private void SetPlayer()
    {
        _playerTransform = GameManager.MainPlayer.transform.parent.GetComponentInChildren<Camera>().transform;
        if (transform == null)
        {
            Debug.Log("player not found by Prompt");
        }
    }


    public void ActivePrompt(GameObject target)
    {
        targetInteractable = target;
        _spriteRenderer.enabled = true;
        var diff = _playerTransform.position - targetInteractable.transform.position;
        var pos = targetInteractable.transform.position + (diff).normalized * (diff.magnitude * promptPosRatio) ;
        //transform.position = pos;
        transform.position = pos;
    }

    public void DeActivePrompt()
    {
        _spriteRenderer.enabled = false;
    }
}
