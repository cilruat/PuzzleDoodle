using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdsManager : ObjectSingletonDontDestroy<AdsManager>, IUnityAdsListener
{
    private static bool _isInit = false;

    //GoogleAdmob
    private BannerView _bannerView;

    private readonly string _bannerUnitId_Android = "ca-app-pub-8148578818392000/7127970422";
    private readonly string _bannerUnitId_Ios = "";

    //UnityAds
    private readonly string _unityAdsGameId_Android = "3881883";
    private readonly string _unityAdsGameId_Ios = "3881882";
    private readonly bool _isTestMode = false;

    private readonly string _stageClearVideoAdId = "stage_clear_video";
    private readonly string _rewardedVideoAdid = "rewardedVideo";

    public bool IsFinishStageClearVideoAds { get; set; }

    public void Init()
    {
        if (_isInit)
            return;

        InitGoogleAds();
        InitUnityAds();

        _isInit = true;
    }

    public void InitGoogleAds()
    {
        MobileAds.Initialize((status) => {});
        ShowBanner();
    }

    public void InitUnityAds()
    {
        Advertisement.AddListener(this);
        string gameId = string.Empty;

#if UNITY_ANDROID
        gameId = _unityAdsGameId_Android;
#elif UNITY_IOS
        gameId = _unityAdsIosGameId;
#endif        
        Advertisement.Initialize(gameId, _isTestMode);
    }

    public void ShowBanner()
    {
        string unitId = string.Empty;
#if UNITY_ANDROID
        unitId = _bannerUnitId_Android;
#elif UNITY_IOS
        unitId = _bannerUnitId_Ios;
#endif     
        _bannerView = new BannerView(unitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();

        _bannerView.LoadAd(request);
    }

    public void ShowStageClearVideoAd()
    {
        if (Advertisement.IsReady(_stageClearVideoAdId))
        {
            Advertisement.Show(_stageClearVideoAdId);
            IsFinishStageClearVideoAds = false;
        }
        else
            IsFinishStageClearVideoAds = true;
    }

    public void ShowRewardedVideoAd()
    {
        if (Advertisement.IsReady(_rewardedVideoAdid))
        {
            Advertisement.Show(_rewardedVideoAdid);
        }
    }

    #region UnityAds

    public void OnUnityAdsReady(string placementId)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == _stageClearVideoAdId)
            OnFinishStageClearVideoAd(showResult);

        if (placementId == _rewardedVideoAdid)
            OnFinishRewardedVideoAd(showResult);
    }

    public void OnUnityAdsDidError(string message)
    {

    }
    #endregion

    #region AD Placement Callback
    void OnFinishStageClearVideoAd(ShowResult result)
    {
        IsFinishStageClearVideoAds = true;
    }

    void OnFinishRewardedVideoAd(ShowResult result)
    {
        //if (result != ShowResult.Finished)
        //    return;

        GameManager.Instance.ShowHint();
    }    
    #endregion
}
