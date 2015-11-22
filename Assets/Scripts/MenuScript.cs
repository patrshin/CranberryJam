using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//The script use for the start and end game menus
public class MenuScript : MonoBehaviour {
	public bool GameOverMenu;
	// Use this for initialization
	void Start () {
		if (GameOverMenu) {
			Text winnerText = GameObject.Find ("Winner Text").GetComponent<Text> ();
			if (winnerText != null) {// If this is null, we are probably on the main menu and there is no winner
				int winner = GetWinner ();
				if (winner != 0) {
					winnerText.text = "PLAYER " + winner + " WINS!";
				} else {
					winnerText.text = "It's a tie!";
				}
			}
		}
	}

	public void LoadLevel1() {
		Application.LoadLevel (1);
	}

	public void LoadLevel2() {
		Application.LoadLevel (2);
	}

	public void LoadLevel3() {
		Application.LoadLevel (3);
	}

	public void BackToMenu() {
		GameObject.Find ("PersistentData").GetComponent<PersistentData>().ClearMatchData ();
		Application.LoadLevel (0);
	}

	public void QuitGame() {
		GameObject.Find ("PersistentData").GetComponent<PersistentData>().ClearMatchData ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
		Application.Quit ();
	}
	//0 = tie, 1 = p1 wins, 2 = p2 wins
	public int GetWinner() {
		int p1Wins = GameObject.Find ("PersistentData").GetComponent<PersistentData>().p1Wins;
		int p2Wins = GameObject.Find ("PersistentData").GetComponent<PersistentData>().p2Wins;
		if (p1Wins > p2Wins) {
			return 1;
		} else if (p1Wins < p2Wins) {
			return 2;
		} else {
			return 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
