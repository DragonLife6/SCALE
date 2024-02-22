using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsRewardedScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button showAdButton;
    [SerializeField] RestartCanvasScript restartScript;
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

        showAdButton.interactable = false;
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        restartScript.StopAllCoroutines();
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if(placementId.Equals(adUnitId))
        {
            //showAdButton.onClick.AddListener(ShowAd);
            showAdButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("Failed To Load!");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Ads Show Click!");
        if (placementId.Equals(adUnitId))
        {
            print("Reward after clicking!");
            restartScript.OnConfirmPressed();
        }
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("Complited!");
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            print("Reward!");
            restartScript.OnConfirmPressed();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        restartScript.OnCancelPressed();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Ads Show Start!");
    }

    void OnDestroy()
    {
        showAdButton.onClick.RemoveAllListeners();
    }
}
