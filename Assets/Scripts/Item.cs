using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public ItemType item;
	public float offset = 0f;

	public Sprite cannonSprite;
	public Sprite dumpSprite;

	// Use this for initialization
	void Start () {
		float rand = Random.Range (0f, 1.0F) + offset;;
		if (rand < 0.3) {
			item = ItemType.CANNON;
			GetComponent<SpriteRenderer>().sprite = cannonSprite;
		} else {
			GetComponent<SpriteRenderer>().sprite = dumpSprite;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
