using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("journal")]
    private bool _isShowingJournal = false;
    public Sprite[] journalEntries;
    public Image _UIJournal;

    [Header("Instructions")] 
    public GameObject escapeMenu;
    
    public Lottie lottie;

    public AudioClip journalopenclip;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _UIJournal.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscapeMenu()
    {
        escapeMenu.gameObject.SetActive(!gameObject.activeSelf);
    }
    public void ShowJournal()
    {
        if (_isShowingJournal)
        {
            //close
            _UIJournal.gameObject.SetActive(false);
            _isShowingJournal = false;
            return;
        }

        if (IllusionManager.instance.currentLevelState < journalEntries.Length)
        {
            _UIJournal.sprite = journalEntries[IllusionManager.instance.currentLevelState];
        }
        if (journalopenclip != null)
        {
            AudioManager.instance.PlayClip( journalopenclip,GameManager.MainPlayer.transform.position);
        }
        _UIJournal.gameObject.SetActive(true);
        _isShowingJournal = true;
    }
    
    
}
