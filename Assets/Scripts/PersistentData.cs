using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

	public int p1Wins;
	public int p2Wins;
	public bool spawnItems;

	void Awake() {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {

	}

	public void ClearMatchData() {
		p1Wins = 0;
		p2Wins = 0;
	}

	public void SetSpawnItemas(bool boo) {
		spawnItems = boo;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
