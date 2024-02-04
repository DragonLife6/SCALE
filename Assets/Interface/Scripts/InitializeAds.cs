using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{

    public string androidGameId;
    public string iosGameId;

    public bool isTestingMode = true;
    string gameId;

    void Awake()
    {
        InitializeAdsMethod();
    }

    private void InitializeAdsMethod()
    {
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId; // Test
#endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        print("Ads initialized!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Ads not initialized!");
    }
}
