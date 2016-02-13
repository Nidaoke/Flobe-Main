/*using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdHandler : MonoBehaviour
{
	public string bannerID, interstitialID;
	public BannerView bannerAd;
	public InterstitialAd interstitialAd;

	//Requests, loads, and displays a banner ad.
	public void RequestBanner(AdSize size, AdPosition pos)
	{
		if(bannerAd != null)
			DestroyBanner();
		bannerAd = new BannerView(bannerID, size, pos);
		bannerAd.LoadAd(new AdRequest.Builder().Build());
	}
	//Unhides banner
	public void ShowBanner()
	{
		if(bannerAd != null)
			bannerAd.Show();
	}
	//Hides banner
	public void HideBanner()
	{
		if(bannerAd != null)
			bannerAd.Hide();
	}
	//Destroys banner
	public void DestroyBanner()
	{
		if(bannerAd != null)
			bannerAd.Destroy();
	}

	//Requests and preloads a new interstitial
	public void RequestInterstitial()
	{
		if(interstitialAd != null)
			DestroyInterstitial();
		interstitialAd = new InterstitialAd(interstitialID);
		interstitialAd.LoadAd(new AdRequest.Builder().Build());
	}
	//Displays the interstitial ad
	public void ShowInterstitial()
	{
		StartCoroutine(LoadThenDisplayInterstitial());
	}
	//Displays the ad as soon as it's ready
	IEnumerator LoadThenDisplayInterstitial()
	{
		while(!interstitialAd.IsLoaded())
		{
			yield return null;
		}
		interstitialAd.Show();
	}
	//Destroys the interstitial instance
	public void DestroyInterstitial()
	{
		StopAllCoroutines();
		interstitialAd.Destroy();
	}
}
*/