using UnityEngine;
using System.Collections;

public class ItemUI : MonoBehaviour {
	SpriteRenderer spr;
	public Sprite none, dump, boost, heavy; 
	public GameObject blimp; 

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (blimp.GetComponent<Blimp>().currItem == ItemType.BOOST) {
			spr.sprite = boost;
		} else if (blimp.GetComponent<Blimp>().currItem == ItemType.DUMP) {
			spr.sprite = dump;
		} else if (blimp.GetComponent<Blimp>().currItem == ItemType.HEAVY) {
			spr.sprite = heavy;
		} else if (blimp.GetComponent<Blimp>().currItem == ItemType.NONE) {
			spr.sprite = none;
		}
	}
}
