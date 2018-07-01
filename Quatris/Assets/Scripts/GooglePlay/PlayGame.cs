using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGame : MonoBehaviour {

	void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance( config );
        PlayGamesPlatform.Activate();
        SignIn();
	}

    void SignIn() {
        Social.localUser.Authenticate( success => { } );
    }

    public static void AddToLeaderBoard(string leaderBoardId, long scores) {
        Social.ReportScore( scores, leaderBoardId, success => { } );
    }

    public static void ShowLeaderboardUI() {
        Debug.Log( "Show leaders" );
        Social.ShowLeaderboardUI();
    }
}
