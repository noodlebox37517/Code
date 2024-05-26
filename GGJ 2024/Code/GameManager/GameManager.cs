using System;
using System.Collections;
using System.Collections.Generic;
using Core.player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Player MainPlayer;
    public GameData gameData;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
            instance = this;

        if (gameData == null)
        {
            Debug.LogWarning($" GameData is null on {gameObject.name}");
        }
    }
}
