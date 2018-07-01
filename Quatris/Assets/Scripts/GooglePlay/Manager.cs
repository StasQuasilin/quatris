using UnityEngine;

public class Manager : MonoBehaviour {

    public static Manager Instance { get; private set; }

	void Start () {
        Instance = this;
	}
	
	public static void Restart(long scores) {
        PlayGame.AddToLeaderBoard( GPGSIds.leaderboard_quatris_leader_board, scores );
    }
}
