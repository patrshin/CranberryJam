using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;
	public GameObject item;
	public float maxDistance; 
	float time = 0.0f;
	float itemTimer = 5f;
	int itemCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		Vector3 p1ViewportCoords = Camera.main.WorldToViewportPoint(p1.transform.position);
		Vector3 p2ViewportCoords = Camera.main.WorldToViewportPoint(p2.transform.position);
		if (p1ViewportCoords.y < 0) {
			Application.LoadLevel(2);
		}
		if (p2ViewportCoords.y < 0) {
			Application.LoadLevel(2);
		}
		if (time > itemTimer && itemCount < 4) {
			float randomX = (Random.value>.5f?-1:1)*Random.Range (0f,7f);
			float randomY = (Random.value>.5f?-1:1)*Random.Range (0f,2f);
			float distance1 = Vector3.Distance(new Vector3(randomX, randomY, 0), p1.transform.position);
			float distance2 = Vector3.Distance(new Vector3(randomX, randomY, 0), p2.transform.position);
			while (distance1 < maxDistance || distance2 < maxDistance) {
				randomX = (Random.value>.5f?-1:1)*Random.Range (0f,7f);
				randomY = (Random.value>.5f?-1:1)*Random.Range (0f,2f);
				distance1 = Vector3.Distance(new Vector3(randomX, randomY, 0), p1.transform.position);
				distance2 = Vector3.Distance(new Vector3(randomX, randomY, 0), p2.transform.position);
			}
			GameObject o = (GameObject)Instantiate (item);
			o.transform.position = new Vector3 (randomX + transform.position.x,
			                                    randomY + transform.position.y, 0);
			time = 0f;
			itemCount++;
		}
	}
}
