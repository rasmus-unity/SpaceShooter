using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    public void ShowAd ()
    {
        Debug.Log ("Show Ad");
		Advertisement.Show ("rewardedVideo");
    }

	public void ShowRewardedAd()
	{
		const string RewardedPlacementId = "rewardedVideo";

		if (!Advertisement.IsReady(RewardedPlacementId))
		{
			Debug.Log(string.Format("Ads not ready for placement '{0}'", RewardedPlacementId));
			return;
		}

		var options = new ShowOptions { resultCallback = HandleShowResult };
		Advertisement.Show(RewardedPlacementId, options);
	}
		
	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			Debug.Log("HERE TAKE SOME DIAMONDS");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

}
