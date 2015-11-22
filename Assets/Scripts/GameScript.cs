using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public Blimp p1;
	public Blimp p2;
	public GameObject item;
	public float maxDistance; 
	float time = 0.0f;
	float itemTimer = 10f;
	int itemCount = 0;
	public Canvas WinUI;
	PersistentData data;

	bool gameOver = false;

	// Use this for initialization
	void Start () {
		data = GameObject.Find ("PersistentData").GetComponent<PersistentData> ();
		WinUI.GetComponent<WinUIScript>().UpdateWinUI (data.p1Wins, data.p2Wins);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (!gameOver) {
			Vector3 p1ViewportCoords = Camera.main.WorldToViewportPoint (p1.transform.position);
			Vector3 p2ViewportCoords = Camera.main.WorldToViewportPoint (p2.transform.position);
			if (p1ViewportCoords.y < -0.1f) {
				gameOver = true;
				data.p2Wins ++;
				AudioSource explosion = p1.GetComponent<AudioSource> ();
				explosion.Play ();
				p1.Bucket.GetComponent<AudioSource>().Play ();
				AudioSource music = GetComponent<AudioSource> ();
				music.Stop ();
				//Destroy (p1);
				p1.transform.position = new Vector3(-1000, -1000, 0);
				Invoke ("TransitionToGameOver", 3.0f);
			}
			if (p2ViewportCoords.y < -0.1f) {
				gameOver = true;
				data.p1Wins ++;
				AudioSource music = GetComponent<AudioSource> ();
				music.Stop ();
				AudioSource explosion = p2.GetComponent<AudioSource> ();
				p2.Bucket.GetComponent<AudioSource>().Play ();
				explosion.Play ();
				p2.transform.position = new Vector3(-1000, -1000, 0);
				//Destroy (p2);
				Invoke ("TransitionToGameOver", 3.0f);
			}
		}
		if (time > itemTimer) {
			float randomX = (Random.value>.5f?-1:1)*Random.Range (0f,4f);
			float randomY = (Random.value>.5f?-1:1)*Random.Range (0f,1.5f);
			float distance1 = Vector3.Distance(new Vector3(randomX, randomY, 0), p1.transform.position);
			float distance2 = Vector3.Distance(new Vector3(randomX, randomY, 0), p2.transform.position);
			while (distance1 < maxDistance || distance2 < maxDistance) {
				randomX = (Random.value>.5f?-1:1)*Random.Range (0f,4f);
				randomY = (Random.value>.5f?-1:1)*Random.Range (0f,1.5f);
				distance1 = Vector3.Distance(new Vector3(randomX, randomY, 0), p1.transform.position);
				distance2 = Vector3.Distance(new Vector3(randomX, randomY, 0), p2.transform.position);
			}
			GameObject o = (GameObject)Instantiate (item);
			o.transform.position = new Vector3 (randomX + transform.position.x,
			                                    randomY + transform.position.y, 0);
			time = 0f;
		}
	}

	void TransitionToGameOver() {;
		if (data.p1Wins >= data.numWins || data.p2Wins >= data.numWins) {
			Application.LoadLevel (4);
		} else {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
