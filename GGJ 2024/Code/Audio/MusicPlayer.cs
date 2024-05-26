using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

   
    public AudioClip[] LevelMusic;
    // Start is called before the first frame update

    private void Awake()
    {
        IllusionManager.StateChanged += TrackChange;
    }

    public void TrackChange(int state)
    {
        if(state >= LevelMusic.Length)
            return;
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playerMusicAudio.clip = LevelMusic[state];
            AudioManager.instance.playerMusicAudio.Play();
        }
    }
    
    private void OnDestroy()
    {
        IllusionManager.StateChanged -= TrackChange;
    }
}
