using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class IllusionManager: MonoBehaviour
{
    public const int TotalLevelStates = 5;
    public static IllusionManager instance;

    public static Action<int> StateChanged;

    public int currentLevelState = 0;
    public AudioClip _newEntryClip;
    public AudioClip confettiClip;
    private bool _isgameOver = false;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        StateChanged?.Invoke(currentLevelState);
    }

    public void ProgressState()
    {
        if (_newEntryClip != null)
        {
            AudioManager.instance.PlayClip( _newEntryClip,GameManager.MainPlayer.transform.position);
        }
        //illusion arrays are not dynamic
        if (currentLevelState + 1 < TotalLevelStates)
        {
            currentLevelState++;
        }
        else if (_isgameOver == false) 
        {
            //load scene
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            _isgameOver = true;
        }

        if (currentLevelState == 2)
        {
            AudioManager.instance.PlayClip( confettiClip,GameManager.MainPlayer.transform.position);
        }
        StateChanged?.Invoke(currentLevelState);
    }
}
