using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public ItemType item;
	public float offset = 0f;

	public Sprite cannonSprite;
	public Sprite dumpSprite;
	public Sprite boostSprite;

	// Use this for initialization
	void Start () {
		float rand = Random.Range (0f, 1.0F) + offset;;
		if (rand < 0.3) {
			item = ItemType.CANNON;
			GetComponent<SpriteRenderer>().sprite = cannonSprite;
		} else {
			GetComponent<SpriteRenderer>().sprite = dumpSprite;
			item = ItemType.DUMP;
		} 
		if (item == ItemType.DUMP) {
			GetComponent<SpriteRenderer>().sprite = dumpSprite;
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		if (item == ItemType.BOOST) {
			GetComponent<SpriteRenderer>().sprite = boostSprite;
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}	if (item == ItemType.CANNON) {
			GetComponent<SpriteRenderer>().sprite = cannonSprite;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
