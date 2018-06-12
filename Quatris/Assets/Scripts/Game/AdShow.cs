using UnityEngine;
using UnityEngine.Advertisements;

public class AdShow : MonoBehaviour {

    public string gameId = "";

	public void Initialize () {
        Debug.Log( "Initialize advertisement for " + gameId );
        Advertisement.Initialize( gameId );
	}

    private bool isShow;

    public void ShowRewardedAd() {
        if (Advertisement.IsReady( "rewardedVideo" )) {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show( "rewardedVideo", options );
            isShow = true;
        }
    }

    private void HandleShowResult(ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
                Debug.Log( "The ad was successfully shown." );
                break;
            case ShowResult.Skipped:
                Debug.Log( "The ad was skipped before reaching the end." );
                break;
            case ShowResult.Failed:
                Debug.LogError( "The ad failed to be shown." );
                break;
        }
        isShow = false;
    }

    public bool IsShow {
        get {
            return isShow;
        }
    }
}
