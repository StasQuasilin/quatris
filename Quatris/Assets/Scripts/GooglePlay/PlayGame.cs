using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGame : MonoBehaviour {

    static void SignIn() {
		if (!Social.localUser.authenticated) {
			PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
			PlayGamesPlatform.InitializeInstance( config );
			PlayGamesPlatform.Activate();
			Social.localUser.Authenticate (success => { });
		}
    }

    public static void AddToLeaderBoard(string leaderBoardId, long scores) {
		SignIn ();
        Social.ReportScore( scores, leaderBoardId, success => { } );
    }

    public static void ShowLeaderboardUI() {
		SignIn ();
        Social.ShowLeaderboardUI();
    }
}
