using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;
using GoogleMobileAds.Api;

/*
            AdSetting.admobRewardedID = "ca-app-pub-3940256099942544/5224354917";
 */
//namespace AdsManagerPlugin.Editor
//{

public enum BannerSize
{
    Banner,
    MediumRectangle,
    IABBanner,
    Leaderboard,
    SmartBanner
}

public class AdsManagerSettings : ScriptableObject
    {
        

        private static AdsManagerSettings instance;

        [SerializeField]
        private bool isTestModeEnabled = false;


        [SerializeField]
        private bool isUnityAdsEnabled = false;

        [SerializeField]
        private string unityAdsId = string.Empty;

        [SerializeField]
        private bool isAdMobEnabled = false;

        [SerializeField]
        private string adMobAndroidAppId = string.Empty;

        [SerializeField]
        private string adMobIOSAppId = string.Empty;

        [SerializeField]
        private bool isLoadingInterstitialEnabled = false;

        [SerializeField]
        private string loadingInterstitalId = string.Empty;

        [SerializeField]
        private string interstitalId = string.Empty;


        [SerializeField]
        private bool isBannerEnabled = false;
    [SerializeField]
    private string bannerId = string.Empty;

    [SerializeField]
    private AdPosition bannerPosition = AdPosition.Top;

    [SerializeField]
    private BannerSize bannerSize = BannerSize.Banner;


    [SerializeField]
        private bool delayAppMeasurementInit = false;

    public BannerSize BannerSize
    {
        get
        {
            return Instance.bannerSize;
        }

        set
        {
            Instance.bannerSize = value;
        }
    }

    public AdPosition BannerPosition
    {
        get
        {
            return Instance.bannerPosition;
        }

        set
        {
            Instance.bannerPosition = value;
        }
    }

    public bool IsBannerEnabled
    {
        get
        {
            return Instance.isBannerEnabled;
        }

        set
        {
            Instance.isBannerEnabled = value;
        }
    }

    public string BannerId
        {
            get
            {
                if (Instance.isTestModeEnabled)
                    return "ca-app-pub-3940256099942544/6300978111";

                return Instance.bannerId;
            }

            set
            {
                if (Instance.isTestModeEnabled) return;
                Instance.bannerId = value;
            }
        }


        public string LoadingInterstitalId
        {
            get
            {
                if (Instance.isTestModeEnabled)
                    return "ca-app-pub-3940256099942544/1033173712";

                return Instance.loadingInterstitalId;
            }

            set
            {
                if (Instance.isTestModeEnabled) return;
                Instance.loadingInterstitalId = value;
            }
        }

        public string InterstitalId
        {
            get
            {
                if (Instance.isTestModeEnabled)
                    return "ca-app-pub-3940256099942544/1033173712";

                return Instance.interstitalId;
            }

            set
            {
                if (Instance.isTestModeEnabled) return;
                Instance.interstitalId = value;
            }
        }

        public bool IsLoadingInterstitialEnabled
        {
            get
            {
                return Instance.isLoadingInterstitialEnabled;
            }

            set
            {
                Instance.isLoadingInterstitialEnabled = value;
            }
        }

        public bool IsTestModeEnabled
        {
            get
            {
                return Instance.isTestModeEnabled;
            }

            set
            {
                Instance.isTestModeEnabled = value;
            }
        }

        public bool IsUnityAdsEnabled
        {
            get
            {
                return Instance.isUnityAdsEnabled;
            }

            set
            {
                Instance.isUnityAdsEnabled = value;
            }
        }

        public string UnityAdsId
        {
            get
            {
                return Instance.unityAdsId;
            }

            set
            {
                Instance.unityAdsId = value;
            }
        }

        public bool IsAdMobEnabled
        {
            get
            {
                return Instance.isAdMobEnabled;
            }

            set
            {
                Instance.isAdMobEnabled = value;
            }
        }

        public string AdMobAndroidAppId
        {
            get
            {
                if (Instance.isTestModeEnabled)
                    return "ca-app-pub-3940256099942544~3347511713";

                return Instance.adMobAndroidAppId;
            }

            set
            {
                if (Instance.isTestModeEnabled) return;
                Instance.adMobAndroidAppId = value;
            }
        }

        public string AdMobIOSAppId
        {
            get
            {
                if (Instance.isTestModeEnabled)
                    return "ca-app-pub-3940256099942544~3347511713";
                return Instance.adMobIOSAppId;
            }

            set
            {
                if (Instance.isTestModeEnabled) return;
                Instance.adMobIOSAppId = value;
            }
        }

        public bool DelayAppMeasurementInit
        {
            get
            {
                return Instance.delayAppMeasurementInit;
            }

            set
            {
                Instance.delayAppMeasurementInit = value;
            }
        }

    public static AdsManagerSettings Instance
    {
        get
        {
            if (instance == null)
            {


                instance = Resources.Load<AdsManagerSettings>("AdsManagerSettings"); //(AdsManagerSettings)Resources.LoadAssetAtPath(
                    //AdsManagerSettingsFile, typeof(AdsManagerSettings));
            }
            return instance;
        }
    }


}
//}
