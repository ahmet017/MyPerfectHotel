using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

public class AdsManager : MonoBehaviour
{
    private string intertestialId = "ca-app-pub-2818378588455414/1053623683";
    private string bannerId = "ca-app-pub-2818378588455414/9292040506";
    private string rewardId = "ca-app-pub-2818378588455414/1413550481";

    private InterstitialAd interstitialAd;
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    private bool giveReward;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        LoadBannerAd();

        RequestInterstitial();

        LoadRewardedAd();
        Debug.Log(intertestialId);
        Debug.Log(bannerId);
        Debug.Log(rewardedAd);

        StartCoroutine(InterstitialAdsShow());

    }
    public int InterstitialTime;
    IEnumerator InterstitialAdsShow()
    {
        yield return new WaitForSeconds(InterstitialTime);
        ShowInterstitialAd();
        yield return new WaitForSeconds(5);
        RequestInterstitial();
        StartCoroutine(InterstitialAdsShow());
    }

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
    }

    public void DestroyAd()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            bannerView.Destroy();
            bannerView = null;
        }
    }
    private void RequestInterstitial()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(intertestialId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            RequestInterstitial();
        }
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                giveReward = true;
                print("Reward");
            });
        }

        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(rewardId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    private void GiveReward()
    {
        FindObjectOfType<GameManager>().AddMoney(50);
    }

    private void Update()
    {
        if (giveReward)
        {
            giveReward = false;
            GiveReward();
        }
    }
}