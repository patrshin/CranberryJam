using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityUI : MonoBehaviour {
	SpriteRenderer spr;
	public Sprite boost; 
	public GameObject blimp; 
	
	Image cooldown;
	
	void Start () {
		GameObject cooldown = GameObject.Find ("CoolDownFinalSprite");

		spr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}
}
