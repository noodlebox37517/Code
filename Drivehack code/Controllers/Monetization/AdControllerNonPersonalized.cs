using System.Collections;
using System.Collections.Generic;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;
using UnityEngine;


public class AdControllerNonPersonalized : MonoBehaviour
{
    public string gameID = "123231";
    public bool testmode = true;
    // Start is called before the first frame update
    void Start()
    {
        Monetization.Initialize(gameID, testmode);
        if (Advertisement.isInitialized)
        {
            Debug.Log("Ads on");
            GameMaster.instance.UI.notifactionText.text = "Ads on";
        }
        else
        {
            GameMaster.instance.UI.notifactionText.text = "adds failed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
