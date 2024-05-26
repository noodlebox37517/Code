using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.SceneManagement;

public class AdController : MonoBehaviour
{
    public bool test = true;
    public string gameID = "3328718";
    public string placementId = "MainBanner";
    public string myPlacementId = "rewardedVideo";
    void Start()
    {
        
        Monetization.Initialize(gameID, test);
        //Advertisement.Initialize(gameID, test);
        
        if (Monetization.isInitialized)
        {
            Debug.Log("Ads on");
            GameMaster.instance.UI.notifactionText.text = "Ads on";
            if (!GameMaster.instance.GetComponent<Stats>().InitialAD)
            {
                //ShowAd(placementId);
                
                GameMaster.instance.GetComponent<Stats>().InitialAD = true;
                //GameMaster.instance.UI.inputAllowed = true;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GameMaster.instance.GetComponent<Stats>().Save();

            }
        }
        else
        {
            GameMaster.instance.UI.notifactionText.text = "adds failed";
        }
        Debug.Log("Active? " + gameObject.activeInHierarchy);
        //Advertisement.Banner.Show("MainBanner");
        StartCoroutine(ShowBannerWhenReady());

    }
    private void Update()
    {
    }
    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
            GameMaster.instance.UI.notifactionText.text = "ad not ready";
        }
        Advertisement.Banner.Show(placementId);
        
        //Monetization.GetPlacementContent(placementId);
       
    }

    

    // Implement IUnityAdsListener interface methods:
    //public void OnUnityAdsReady(string placementId)
    //{
    //    // If the ready Placement is rewarded, activate the button: 

    //}

    //public void OnUnityAdsDidFinish(string placementId, UnityEngine.Monetization.ShowResult showResult)
    //{
    //    // Define conditional logic for each ad completion status:
    //    if (showResult == UnityEngine.Monetization.ShowResult.Finished)
    //    {
    //        // Reward the user for watching the ad to completion.
    //        GameMaster.instance.ResetScene();
    //    }
    //    else if (showResult == UnityEngine.Monetization.ShowResult.Skipped)
    //    {
    //        // Do not reward the user for skipping the ad.
    //    }
    //    else if (showResult == UnityEngine.Monetization.ShowResult.Failed)
    //    {
    //        Debug.LogWarning("The ad did not finish due to an error.");
    //    }
    //}

    //public void OnUnityAdsDidError(string message)
    //{
    //    // Log the error.
    //}

    //public void OnUnityAdsDidStart(string placementId)
    //{
    //    // Optional actions to take when the end-users triggers an ad.
    //}
    public void ShowAd(string placeid)
    {
        StartCoroutine(ShowAdWhenReady(placeid));
    }

    private IEnumerator ShowAdWhenReady(string placementId)
    {
        Debug.Log("attempt to show ad " + placementId);
        GameMaster.instance.UI.notifactionText.text = "Ads attempt "+ placementId;
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        Debug.Log(" show ad " + placementId);
        if (ad != null)
        {
            ad.Show();
        }
    }
    //private void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //{
    //    if (showResult == ShowResult.Finished)
    //    {
    //        // Reward the user for watching the ad to completion.
    //        Debug.Log("ADDDDDDDDDDDDDDs");
    //    }
    //    else if (showResult == ShowResult.Skipped)
    //    {
    //        // Do not reward the user for skipping the ad.
    //        Debug.Log("ADDDDDDDDDDDDDDs");
    //    }
    //    else if (showResult == ShowResult.Failed)
    //    {
    //        Debug.LogWarning("The ad did not finish due to an error.");
    //    }
    //}


}

