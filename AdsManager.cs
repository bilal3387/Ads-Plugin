using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

using UnityEngine.Advertisements;

public enum RewardedAdResult
{
    Finished,
    Failed,
    Skipped,
    Unknown
}

public sealed class AdsManager : IUnityAdsListener
{
    private static AdsManager instance = null;
    private static readonly object padlock = new object();

    public delegate void InterstitialDidFinishOrFail();
    public event InterstitialDidFinishOrFail OnLoadingInterstitialDidFinishOrFail;

    public delegate void RewardedAdEvent(RewardedAdResult result, string message);
    public event RewardedAdEvent OnRewardedAdDidFinish;

    private InterstitialAd interstitial;
    private bool isLoadingInterstitial = false, shoulRequestInterstitial = false;

    private BannerView bannerView;


    private void RequestInterstitial(string interstitalId)
    {
        this.interstitial = new InterstitialAd(interstitalId);

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        shoulRequestInterstitial = false;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if (this.isLoadingInterstitial)
        {
            this.interstitial.Show();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        if (this.isLoadingInterstitial)
        {
            this.isLoadingInterstitial = false;
            if(OnLoadingInterstitialDidFinishOrFail != null)
            this.OnLoadingInterstitialDidFinishOrFail.Invoke();
        }

        shoulRequestInterstitial = true;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if (this.isLoadingInterstitial)
        {
            this.isLoadingInterstitial = false;
            if(OnLoadingInterstitialDidFinishOrFail != null)
            this.OnLoadingInterstitialDidFinishOrFail.Invoke();
        }

        RequestInterstitial(AdsManagerSettings.Instance.InterstitalId);
    }

    public void ShowAdmobIntersitial()
    {
        if (this.interstitial != null && this.interstitial.IsLoaded())
            this.interstitial.Show();
        else if (shoulRequestInterstitial)
            RequestInterstitial(AdsManagerSettings.Instance.InterstitalId);
    }

    public bool IsAdmobInterstitialAvailable
    {
        get
        {
            return interstitial != null && interstitial.IsLoaded();
        }
    }

    public void ShowAdmobBanner()
    {
        if (!AdsManagerSettings.Instance.IsBannerEnabled)
        {
            Debug.LogError("Banner not enabled");
        }

        if (this.bannerView != null)
            this.bannerView.Show();
    }

    public void HideAdmobBanner()
    {
        if (!AdsManagerSettings.Instance.IsBannerEnabled)
        {
            Debug.LogError("Banner not enabled");
        }

        if (this.bannerView != null)
            this.bannerView.Hide();
    }

    private void RequestBanner()
    {
        AdSize adSize = AdSize.Banner;
        switch (AdsManagerSettings.Instance.BannerSize)
        {
            case BannerSize.MediumRectangle:
                adSize = AdSize.MediumRectangle;
                break;

            case BannerSize.IABBanner:
                adSize = AdSize.IABBanner;
                break;

            case BannerSize.Leaderboard:
                adSize = AdSize.Leaderboard;
                break;

            case BannerSize.SmartBanner:
                adSize = AdSize.SmartBanner;
                break;
        }
        this.bannerView = new BannerView(AdsManagerSettings.Instance.BannerId, adSize, AdsManagerSettings.Instance.BannerPosition);

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
        this.bannerView.Hide();
    }

    AdsManager()
    {
        if (AdsManagerSettings.Instance.IsAdMobEnabled)
        {
            MobileAds.Initialize(initStatus => { });

            if (AdsManagerSettings.Instance.IsLoadingInterstitialEnabled)
            {
                isLoadingInterstitial = true;
                RequestInterstitial(AdsManagerSettings.Instance.LoadingInterstitalId);
            }
            else
                RequestInterstitial(AdsManagerSettings.Instance.InterstitalId);

            if (AdsManagerSettings.Instance.IsBannerEnabled)
            {
                RequestBanner();
            }
        }

        if (AdsManagerSettings.Instance.IsUnityAdsEnabled)
        {
            Advertisement.Initialize(AdsManagerSettings.Instance.UnityAdsId, AdsManagerSettings.Instance.IsTestModeEnabled);
            Advertisement.AddListener(this);
        }
    }

    public bool IsUnityAdAvailable
    {
        get
        {
            return Advertisement.isInitialized && Advertisement.IsReady();
        }
    }

    public bool IsUnityRewardedAdAvailable
    {
        get
        {
            return Advertisement.isInitialized && Advertisement.IsReady("rewardedVideo");
        }
    }

    public void ShowUnityAd()
    {
        if (Advertisement.isInitialized && Advertisement.IsReady())
            Advertisement.Show();
    }

    public void ShowUnityRewardedAd(RewardedAdEvent rewardedAdEvent)
    {
        if (Advertisement.isInitialized && Advertisement.IsReady("rewardedVideo"))
        {
            OnRewardedAdDidFinish = rewardedAdEvent;
            Advertisement.Show("rewardedVideo");
        }
    }



    public void OnUnityAdsReady(string placementId)
    {
        //Instance.OnUnityAdsReady(placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        //Instance.OnUnityAdsDidError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Instance.OnUnityAdsDidStart(placementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId.Equals("rewardedVideo"))
        {
            RewardedAdResult result = RewardedAdResult.Unknown;
            switch(showResult)
            {
                case ShowResult.Finished:
                    result = RewardedAdResult.Finished;
                    break;

                case ShowResult.Failed:
                    result = RewardedAdResult.Failed;
                    break;

                case ShowResult.Skipped:
                    result = RewardedAdResult.Skipped;
                    break;
            }
            if(OnRewardedAdDidFinish != null)
            OnRewardedAdDidFinish.Invoke(result, "");
        }
    }

    public static AdsManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new AdsManager();
                }
                return instance;
            }
        }
    }
}