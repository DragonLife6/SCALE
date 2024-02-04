using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsBannerScript : MonoBehaviour
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
}
