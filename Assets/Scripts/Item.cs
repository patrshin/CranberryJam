using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public ItemType item;

	// Use this for initialization
	void Start () {
		float rand = Random.Range (0f, 1.0F);
		if (rand < 0.5) {
			item = ItemType.BOOST;
		} else if (rand < 0.8) {
			item = ItemType.DUMP;
		} else {
			item = ItemType.HEAVY;
		}
		if (item == ItemType.DUMP) {
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		if (item == ItemType.BOOST) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}
		if (item == ItemType.HEAVY) {
			gameObject.GetComponent<Renderer> ().material.color = Color.blue;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
