using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

public class ReardedAdButton : MonoBehaviour
{
    // Start is called before the first frame update
    Button myButton;
    public string myPlacementId = "rewardedVideo";

    void Start()
    {
        myButton = GetComponent<Button>();

    }
    public void ShowRewardedVideo()
    {
        if (Monetization.isInitialized)
        {
            Debug.Log("attempt ad");
            StartCoroutine(WaitForAd());
        }
    }
    public void ShowAd(string placeid)
    {
        StartCoroutine(ShowAdWhenReady(placeid));
    }

    private IEnumerator ShowAdWhenReady(string placementId)
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }
    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(myPlacementId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(myPlacementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
            //give 3 lives and temp invul back to level, level play
            GameMaster.instance.life.LifeAdd(3);
            GameMaster.instance.LC.LevelPlay = true;
            Debug.Log("lifes charged");
            //update loss values
            //GameMaster.instance.UI.EndScreenUpdate();
            GameMaster.instance.UI.SwitchPanels(0);
            //GameMaster.instance.ResetScene();
        }
    }

    // Implement a function for showing a rewarded video ad:

}

