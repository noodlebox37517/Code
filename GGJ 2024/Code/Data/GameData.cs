using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DATA", order = 1)]
public class GameData : ScriptableObject
{

    [Header("Interactable settings")] 
    public float DefualtInteractableTriggerRadius = 2f;

    [Header("Clip")] [SerializeField]
    public AudioClip onEnterClip;
    
    [Header("Padlock Code")] [SerializeField]
    public int Code = 0000;
}
