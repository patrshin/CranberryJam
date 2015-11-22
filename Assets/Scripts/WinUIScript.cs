using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinUIScript : MonoBehaviour {

	public Text p1Text;
	public Text p2Text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateWinUI(int p1Wins, int p2Wins) {
		p1Text.GetComponent<Text> ().text = p1Wins + "";
		p2Text.GetComponent<Text> ().text = p2Wins + "";
	}
}
