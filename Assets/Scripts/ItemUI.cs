using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemUI : MonoBehaviour {
	SpriteRenderer spr;
	public Sprite none, dump, cannon; 
	public GameObject blimp; 
	
	void Start () {
		spr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (blimp.GetComponent<Blimp> ().currItem == ItemType.DUMP) {
			spr.sprite = dump;
		} else if (blimp.GetComponent<Blimp> ().currItem == ItemType.CANNON) {
			spr.sprite = cannon;
		} else {
			spr.sprite = none;
		}
	}
}
