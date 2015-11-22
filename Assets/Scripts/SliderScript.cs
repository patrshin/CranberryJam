using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderScript : MonoBehaviour {

	public PersistentData data;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SliderChanged() {
		int numWins = (int)GetComponent<Slider> ().value;
		data.SetNumWins (numWins);
		if (numWins > 1) {
			GetComponentInChildren<Text>().text = "Play to " + numWins + " wins";
		} else {
			GetComponentInChildren<Text>().text = "Play to " + numWins + " win";
		}
	}
}
