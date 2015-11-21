using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p1ViewportCoords = Camera.main.WorldToViewportPoint(p1.transform.position);
		Vector3 p2ViewportCoords = Camera.main.WorldToViewportPoint(p2.transform.position);
		if (p1ViewportCoords.y < 0) {
			Application.LoadLevel(2);
		}
		if (p2ViewportCoords.y < 0) {
			Application.LoadLevel(2);
		}
	}
}
