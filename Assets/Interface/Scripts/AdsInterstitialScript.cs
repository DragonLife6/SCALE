using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInterstitialScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string androidAdUnitId;
    public string iosAdUnitId;

    string adUnitId;

    void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        adUnitId = androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Loaded!");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("Failed To Load!");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Ads Show Click!");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("Ads Show Complete!");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Ads Show Failure!");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Ads Show Start!");
    }
}
