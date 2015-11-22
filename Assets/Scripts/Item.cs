﻿using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public ItemType item;
	public float offset = 0f;

	// Use this for initialization
	void Start () {
		float rand = Random.Range (0f, 1.0F) + offset;;
		if (rand < 0.3) {
			item = ItemType.CANNON;
		} else {
			item = ItemType.DUMP;
		} 
		if (item == ItemType.DUMP) {
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
		if (item == ItemType.BOOST) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		}	if (item == ItemType.CANNON) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
