using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{

    [FormerlySerializedAs("playerAudio")] public AudioSource playerMusicAudio;
    public static AudioManager instance;
    public const int maxsources = 5;
    private int _nextSource = 0; 
    public List<AudioSource> audiosources;

    public AudioClip _onstartclip;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        if (  playerMusicAudio == null)
        {
            //eww messed up oop
            playerMusicAudio =  GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        for (int i = 0; i < maxsources; i++)
        {
            var go =Instantiate( new GameObject("audio source " + i));
            var source = go.AddComponent<AudioSource>();
            audiosources.Add(source);
        }
    }

    private void Start()
    {
        if (_onstartclip != null)
        {
            PlayClip(_onstartclip, GameManager.MainPlayer.transform.position);
        }
    }

    public void PlayClip(AudioClip clip, Vector3 pos)
    {
        if (clip == null)
        {
            return;
        }
        audiosources[_nextSource].clip = clip;
        audiosources[_nextSource].Play();
        audiosources[_nextSource].gameObject.transform.position = pos;
        _nextSource++;
        if (_nextSource > audiosources.Count - 1)
        {
            _nextSource = 0;
        }
    }
    
}
